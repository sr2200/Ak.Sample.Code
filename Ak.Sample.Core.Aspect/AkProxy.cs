﻿using System;
using System.Diagnostics;
using System.Reflection;

namespace Ak.Sample.Core.Aspect
{
    public class AkProxy<T> : DispatchProxy
    {
        private T _instance;

        public static T Create(T instance)
        {
            object proxy = Create<T, AkProxy<T>>();
            ((AkProxy<T>)proxy).SetParameters(instance);

            return (T)proxy;
        }

        protected void SetParameters(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            _instance = instance;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Guid msgId = Guid.NewGuid();
            try
            {
                BeforeProcess(msgId, targetMethod, args);
                var result = targetMethod.Invoke(_instance, args);
                AfterProcess(msgId, targetMethod, args, result);

                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                ExceptionProcess(msgId, targetMethod, args, ex);
                throw ex.InnerException ?? ex;
            }
        }

        protected void BeforeProcess(Guid msgId, MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"[{msgId.ToString()}][Method：{targetMethod.Name}] 処理を実行します。");
        }

        protected void AfterProcess(Guid msgId, MethodInfo targetMethod, object[] args, object result)
        {
            Console.WriteLine($"[{msgId.ToString()}][Method：{targetMethod.Name}] 処理が正常終了しました。");
        }

        protected void ExceptionProcess(Guid msgId, MethodInfo targetMethod, object[] args, Exception ex)
        {
            Console.WriteLine($"[{msgId.ToString()}][Method：{targetMethod.Name}] 例外が発生しました。");
            Console.WriteLine($"[{ex.ToString()}]");
        }
    }
}
