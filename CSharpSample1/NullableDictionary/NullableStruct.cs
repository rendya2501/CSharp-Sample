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
    public struct Nullable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
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
        public override bool Equals(object obj) => obj is Nullable<T> nullable && (ReferenceEquals(Value, nullable.Value) || Value.Equals(nullable.Value));
        public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();

    }
}
