using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System;

namespace Rikarin.Filtering {
    public static class FilterExtensions {
        public static IEnumerable<T> FilterEnum<T>(this IEnumerable<T> query, IEnumerable<Filter> filter) {
            if (filter == null) {
                return query;
            }

            foreach (var f in filter) {
                switch (f.Condition) {
                    case FilterCondition.IsEmpty:
                        query = query.Where(x => String.IsNullOrWhiteSpace(GetValue(x, f.Column).ToString()));
                        break;

                    case FilterCondition.IsNotEmpty:
                        query = query.Where(x => !String.IsNullOrWhiteSpace(GetValue(x, f.Column).ToString()));
                        break;

                    case FilterCondition.IsEqual:
                        query = query.Where(x => Convert.ToString(GetValue(x, f.Column), CultureInfo.InvariantCulture) == f.Value);
                        break;

                    case FilterCondition.IsNotEqual:
                        query = query.Where(x => Convert.ToString(GetValue(x, f.Column), CultureInfo.InvariantCulture) != f.Value);
                        break;

                    case FilterCondition.BeginsWith:
                        query = query.Where(x => GetValue(x, f.Column).ToString().StartsWith(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.EndsWith:
                        query = query.Where(x => GetValue(x, f.Column).ToString().EndsWith(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.Contains:
                        query = query.Where(x => GetValue(x, f.Column).ToString().Contains(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.DoesNotContains:
                        query = query.Where(x => !GetValue(x, f.Column).ToString().Contains(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.IsGreather:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) > Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsGreatherOrEqual:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) >= Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsLess:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) < Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsLessOrEqual:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) <= Convert.ToInt64(f.Value));
                        break;
                }
            }

            return query;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, IEnumerable<Filter> filter) {
            if (filter == null) {
                return query;
            }

            foreach (var f in filter) {
                switch (f.Condition) {
                    case FilterCondition.IsEmpty:
                        query = query.Where(x => String.IsNullOrEmpty(GetValue(x, f.Column).ToString()));
                        break;

                    case FilterCondition.IsNotEmpty:
                        query = query.Where(x => !String.IsNullOrEmpty(GetValue(x, f.Column).ToString()));
                        break;

                    case FilterCondition.IsEqual:
                        query = query.Where(x => Convert.ToString(GetValue(x, f.Column), CultureInfo.InvariantCulture) == f.Value);
                        break;

                    case FilterCondition.IsNotEqual:
                        query = query.Where(x => Convert.ToString(GetValue(x, f.Column), CultureInfo.InvariantCulture) != f.Value);
                        break;

                    case FilterCondition.BeginsWith:
                        query = query.Where(x => GetValue(x, f.Column).ToString().StartsWith(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.EndsWith:
                        query = query.Where(x => GetValue(x, f.Column).ToString().EndsWith(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.Contains:
                        query = query.Where(x => GetValue(x, f.Column).ToString().Contains(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.DoesNotContains:
                        query = query.Where(x => !GetValue(x, f.Column).ToString().Contains(f.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;

                    case FilterCondition.IsGreather:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) > Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsGreatherOrEqual:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) >= Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsLess:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) < Convert.ToInt64(f.Value));
                        break;

                    case FilterCondition.IsLessOrEqual:
                        query = query.Where(x => Convert.ToInt64(GetValue(x, f.Column)) <= Convert.ToInt64(f.Value));
                        break;
                }
            }

            return query;
        }

        static object GetValue<T>(T obj, string property) {
            var props = property.Split('.');
            var type = typeof(T);
            object ret = obj;

            foreach (var x in props) {
                var prop = type.GetProperty(x.First().ToString().ToUpper() + x.Substring(1));
                type = prop.PropertyType;

                if (ret == null) {
                    throw new Exception($"Property {property} is null on object {typeof(T).Name}");
                }
                ret = prop.GetValue(ret);
            }

            if (ReferenceEquals(ret, obj)) {
                throw new Exception($"Property {property} doesn't exist on object {typeof(T).Name}");
            }

            return ret;
        }

        public static void AllowedColumns(this IEnumerable<Filter> filters, params string[] columns) {
            var invalid = new List<string>(); 

            foreach (var x in filters) {
                if (!columns.Contains(x.Column)) {
                    invalid.Add(x.Column);
                }
            }

            if (invalid.Count != 0) {
                throw new FilterException(invalid.ToArray());
            }
        }
    }
}
