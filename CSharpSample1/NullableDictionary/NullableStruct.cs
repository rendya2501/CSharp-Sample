using System;
using System.Diagnostics.CodeAnalysis;

namespace NullableDictionary
{
    /// <summary>
    /// Dictionaryのキーにnullを許容させるための構造体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>    /// 
    /// 構造体はnullになりえない性質なので、
    /// null要素を構造体にラップさせることでDictionaryを騙す。
    /// </remarks>
    public struct Nullable<T> : IEquatable<Nullable<T>>
    {
        /// <summary>
        /// 入力値
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// 不要なNewを避けるためPrivateで宣言する。
        /// </remarks>
        public Nullable(T value) => Value = value;

        public static bool operator ==(Nullable<T> x, Nullable<T> y) => x.Equals(y);
        public static bool operator !=(Nullable<T> x, Nullable<T> y) => !x.Equals(y);
        public static implicit operator T(Nullable<T> source) => source.Value;
        public static implicit operator Nullable<T>(T source) => new Nullable<T>(source);

        public override string ToString() => Value?.ToString();
        //public override bool Equals(object obj)
        //{
        //    //if (obj is Nullable<T> nullable)
        //    //{
        //    //    var n = nullable.Value;
        //    //    return ReferenceEquals(Value, n) || Value.Equals(n);
        //    //}
        //    //return false;
        //}

        public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();
        public override bool Equals(object obj) => obj is Nullable<T> nullable && Equals(nullable);
        public bool Equals([AllowNull] Nullable<T> nullable) => ReferenceEquals(Value, nullable.Value) || Value.Equals(nullable.Value);
    }
}
