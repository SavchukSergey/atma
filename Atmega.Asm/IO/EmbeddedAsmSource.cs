﻿using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Atmega.Asm.IO {
    public class EmbeddedAsmSource : IAsmSource {
        private readonly Assembly _assembly;
        private readonly string _prefix;

        public EmbeddedAsmSource(Assembly assembly, string prefix) {
            _assembly = assembly;
            _prefix = prefix;
        }

        public string LoadContent(string fileName) {
            var name = GetFullName(fileName);
            var stream = _assembly.GetManifestResourceStream(name);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public string ResolveFile(string fileName, string referrer) {
            fileName = FormatName(fileName);
            referrer = FormatName(referrer);
            var basePath = referrer != null ? GetDirectoryName(referrer) : null;
            if (basePath != null) {
                var path = Combine(basePath, fileName);
                if (FileExists(path)) return path;
            }

            return null;
        }

        private static string FormatName(string fileName) {
            return fileName
                .Replace("\\", ".")
                .Replace("/", ".");
        }

        private bool FileExists(string fileName) {
            var name = GetFullName(fileName);
            var stream = _assembly.GetManifestResourceStream(name);
            return stream != null;
        }

        private string GetFullName(string name) {
            return Combine(_prefix, name);
        }

        private static string GetDirectoryName(string path) {
            var extIndex = path.LastIndexOf('.');
            path = path.Substring(0, extIndex);
            var dirIndex = path.LastIndexOf('.');
            if (dirIndex < 0) return "";
            path = path.Substring(0, dirIndex);
            return path;
        }

        private static string Combine(string path1, string path2) {
            path1 = path1.TrimEnd('.');
            path2 = path2.TrimStart('.');
            if (string.IsNullOrWhiteSpace(path1)) return path2;
            if (string.IsNullOrWhiteSpace(path2)) return path1;
            return path1 + "." + path2;
        }
    }
}
