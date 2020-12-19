using PasswordCreator;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace BlankCoreApp1.ViewModels
{
    /// <summary>
    /// https://qiita.com/java1031/items/c294c9e292a0f18a02d9#2-prism%E3%81%AB%E3%82%88%E3%82%8B%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0
    /// </summary>
    public class MainWindowViewModelWithReactiveProperty : BindableBase
    {
        #region メンバ
        private readonly CompositeDisposable _cd = new CompositeDisposable();
        private ILetterFactory factory;
        #endregion

        #region プロパティ
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Prism Application");

        public ReactiveProperty<bool> IsNonMark { get; private set; } = new ReactiveProperty<bool>(false);

        public ReactiveProperty<int> NumOfLetters { get; private set; } = new ReactiveProperty<int>(20);

        public ReactiveProperty<string> CreatePassword { get; private set; } = new ReactiveProperty<string>("Prism+ReactiveProperty WPF App.");

        public ReactiveCommand Generate { get; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModelWithReactiveProperty()
        {
            // ReactivePropertyやReactiveCommandはIDisposableを継承しているため，使用後はDisposeする必要があるとのこと。
            // これもまとめてDisposeできるようにコンストラクタでCompositeDisposable型の変数に登録している
            this.Title.AddTo(_cd);
            this.NumOfLetters.AddTo(_cd);
            this.CreatePassword.AddTo(_cd);
            this.IsNonMark.AddTo(_cd);
            // コマンドの状態を監視し，コマンド実行（ボタン押下）時に所定のメソッド実行することを登録する
            IsNonMark.Subscribe(_ => SetFactory());
            // 所定の条件を満たす場合、コマンドを有効化する
            Generate = NumOfLetters.Select(x => x > 10).ToReactiveCommand();
            // C# 9.0より導入されたラムダ式の破棄を使っているらしい
            // 違った。破棄は2つ以上の引数で連続して[_]を使ったときの話で、1つだけの場合は[_]という名前扱いになるらしい。
            // つまり、どうでもいい名前を付けたい場合にこうすればよいということだ。
            // https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/lambda-expressions
            Generate.Subscribe(_ => CreatePasswordExecute());
        }


        #endregion

        #region メソッド
        private void CreatePasswordExecute()
        {
            Random random = new Random();
            var generator = new PasswordGenerator(random);
            CreatePassword.Value = generator.MakePassword(NumOfLetters.Value, factory);
        }

        private void SetFactory() => factory = IsNonMark.Value
                ? (ILetterFactory)new NonMarkLetterFactory()
                : new AllLetterFactory();
        public void Dispose() => this._cd.Dispose();
        #endregion
    }
}
