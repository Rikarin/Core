namespace Rikarin.Domain {
    public abstract class Entity : Entity<long> {
        
    }

    public abstract class Entity<T> : IEntity<T> {
        private int? _mRequestedHashCode;

        public T Id { get; set; }
        public bool IsTransient => default(T).Equals(Id);

        public override bool Equals(object obj) {
            if (!(obj is Entity<T>)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (GetType() != obj.GetType()) {
                return false;
            }

            var item = (Entity<T>)obj;
            if (item.IsTransient || IsTransient) {
                return false;
            }

            return item.Id.Equals(Id);
        }

        public override int GetHashCode() {
            if (IsTransient) {
                return base.GetHashCode();
            }

            if (!_mRequestedHashCode.HasValue) {
                _mRequestedHashCode = Id.GetHashCode() ^ 31;
            }

            return _mRequestedHashCode.Value;
        }

        public static bool operator ==(Entity<T> left, Entity<T> right) {
            return Equals(left, null) ? Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right) {
            return !(left == right);
        }
    }
}
