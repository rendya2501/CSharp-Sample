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
        /// Dictionaryからコンストラクタが呼び出されるのではなく、implicit operator経由でたどり着く。
        /// </remarks>
        public Nullable(T value) => Value = value;

        /// <summary>
        /// 演算子のオーバーロード。
        /// == 演算子を使ってxとyの比較を行った場合に呼び出される。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Nullable<T> x, Nullable<T> y) => x.Equals(y);
        /// <summary>
        /// 演算子のオーバーロード。
        /// =! 演算子を使ってxとyの比較を行った場合に呼び出される。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Nullable<T> x, Nullable<T> y) => !x.Equals(y);
        /// <summary>
        /// Nullable<T>型をT型に変換します。
        /// 「var b1 = new Nullable<double>(1); var b2 = (int)b1;」等のキャストを行ったときに呼び出される。
        /// explicit:明示的にキャストしないとエラー。new Dictionary<Nullable<int?>, string>(){(Nullable<int?>)null, "null" }→こうしないといけない。
        /// implicit:明示的にキャストしなくてもエラーにならない。 new Dictionary<Nullable<int?>, string>(){null, "null" }→これでもOK。
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator T(Nullable<T> source) => source.Value;
        /// <summary>
        /// T型をNullable<T>型に変換します。
        /// 変換するT型の値をコンストラクタの引数としてNullable<T>型を生成することでT型をNullable<T>型に変換します。
        /// 「(Nullable<T>)null」 等のキャストを行ったときに呼び出される。
        /// </summary>
        /// <param name="source"></param>
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
