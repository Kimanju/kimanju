using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Helpers;

namespace Kimanju {
    public class Labelable : Nameable {
        private readonly List<String> _labels = new List<String>();

        public ReadOnlyCollection<String> Labels { get { return _labels.AsReadOnly(); } }

        public Labelable(String name)
        : base(name) {

        }

        public Labelable(String name, List<String> labels)
        : this(name) {
            Objects.RequireNonNull(labels, "Labels list can't be null.");

            labels.Where(l => l != null && l.Length > 0).Distinct().OrderBy(l => l).ToList().ForEach(l => _labels.Add(l));
        }

        public void AddLabel(String label) {
            Objects.RequireNonNull(label, "Label to add can't be null.");
            Objects.Check(label, l => l.Length > 0, "Label can't be empty.");
            
            if (_labels.Contains(label)) {
                throw new ArgumentException(String.Format("Label '{0}' has already been added.", label));
            }

            var index = _labels.BinarySearch(label);

            _labels.Insert(index < 0 ? ~index : index, label);
        }

        public void RemoveLabel(String label) {
            Objects.RequireNonNull(label, "Label to find can't be null.");
            
            if (!_labels.Remove(label)) {
                throw new ArgumentException(String.Format("Unknown label '{0}'.", label));
            }
        }

        public void replaceLabel(String oldLabel, String newLabel) {
            RemoveLabel(oldLabel);
            AddLabel(newLabel);
        }
    }
}