using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ijw.Next.DDD {
    /// <summary>
    /// 属性变更通知修改的实体基类
    /// </summary>
    public abstract class NotifyPropertyChangeBase : INotifyPropertyChanged, INotifyPropertyChanging, IWhenPropertyChanges {
        /// <summary>
        /// 属性值即将赋值
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        /// <summary>
        /// 属性值赋值之后
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性值进行变化之前, 可以取消赋值
        /// </summary>
        public event BeforePropertyChangingEventHandler BeforePropertyValueChanges;
        /// <summary>
        /// 属性值变化之后
        /// </summary>
        public event AfterPropertyChangedEventHandler AfterPropertyValueChanged;

#if NET35 || NET40 || NET45
        /// <summary>
        /// 属性通用的setter方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="property"></param>
        protected virtual void set<T>(ref T property, T value) {
            string propertyName = DebugHelper.GetCallerMethod().Name.Remove(0, 4);
#else
        /// <summary>
        /// 属性通用的setter方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <param name="propertyName"></param>
        protected virtual void set<T>(ref T property, T value, [CallerMemberName]string propertyName = "") {
            propertyName.ShouldNotBeNullOrEmpty();
#endif
            if (!Equals(property, value)) {
                if (BeforePropertyValueChanges != null || AfterPropertyValueChanged != null) {
                    var oldvalue = property;
                    var evntArgs = new PropertyValueChangeEventArgs(propertyName, oldvalue, value);
                    bool allowChanging = raiseBeforePropertyValueChangesEvent(evntArgs);
                    if (!allowChanging) return;
                    setvalue(ref property, value, propertyName);
                    raiseAfterPropertyValueChangesEvent(evntArgs);
                }
                else {
                    setvalue(ref property, value, propertyName);
                }
            }
        }

        private bool raiseBeforePropertyValueChangesEvent(PropertyValueChangeEventArgs evntArgs) {
            var evntHndlrs = BeforePropertyValueChanges.GetInvocationList();
            foreach (var evntHndlr in evntHndlrs) {
                var handler = (BeforePropertyChangingEventHandler)evntHndlr;
                var result = handler(this, evntArgs);
                if (result == false) return false;
            }
            return true;
        }

        private void raiseAfterPropertyValueChangesEvent(PropertyValueChangeEventArgs evntArgs) {
            AfterPropertyValueChanged?.Invoke(this, evntArgs);
        }

        private void setvalue<T>(ref T property, T value, string propertyName) {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
