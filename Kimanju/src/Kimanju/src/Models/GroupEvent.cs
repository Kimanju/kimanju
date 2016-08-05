using System;
using System.Collections.Generic;
using Helpers;

namespace Kimanju {
    public class GroupEvent : Labelable {
        private readonly Restaurant _place;
        private readonly DateTime _begin;

        public Restaurant Place { get { return _place; } }
        public DateTime Begin { get { return _begin; } }

        public GroupEvent(String name, Restaurant place, DateTime begin, List<String> labels)
        : base(name, labels) {
            this._place = Objects.RequireNonNull(place, "Event place can't be null.");
            Objects.RequireNonNull(begin, "Event begin date can't be null.");
            this._begin = Objects.Check(begin, d => d.CompareTo(DateTime.Now) < 0, "Event begin date can't be in the past.");
        }

        public bool OccursToday() {
            var today = DateTime.Now;

            return _begin.Year == today.Year
            &&     _begin.Month == today.Month
            &&     _begin.Day == today.Day;
        }
    }
}