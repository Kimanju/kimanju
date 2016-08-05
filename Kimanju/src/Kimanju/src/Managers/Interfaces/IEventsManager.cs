using System;

namespace Kimanju {
    public interface IEventsManager {
        void Add(GroupEvent groupEvent);
        void Remove(GroupEvent groupEvent);
        void Remove(String name);
        void RemoveAll(Restaurant restaurant);
        void RemoveAll(DateTime beginDate);
        void RemoveAll(User user);
        void RemoveIf(Func<Restaurant, Boolean> predicate);
        void RemoveAll();
    }
}