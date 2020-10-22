using PasswordCreator;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace BlankCoreApp1.ViewModels
{
    public class MainWindowViewModelWithReactiveProperty : BindableBase
    {
        #region メンバ
        private readonly CompositeDisposable _cd = new CompositeDisposable();
        private ILetterFactory factory;
        #endregion

        #region プロパティ
        //private string _title = "Prism Application";
        //public string Title
        //{
        //    get => _title;
        //    set => SetProperty(ref _title, value);
        //}
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Prism Application");

        //private bool isNonMark = false;
        //public bool IsNonMark
        //{
        //    get => isNonMark;
        //    set
        //    {
        //        SetProperty(ref isNonMark, value);
        //        factory = isNonMark
        //            ? (ILetterFactory)new NonMarkLetterFactory()
        //            : new AllLetterFactory();
        //    }
        //}
        public ReactiveProperty<bool> IsNonMark { get; private set; } = new ReactiveProperty<bool>(false);


        //private int numOfLetters;
        //public int NumOfLetters
        //{
        //    get => numOfLetters;
        //    set => SetProperty(ref numOfLetters, value);
        //}
        public ReactiveProperty<int> NumOfLetters { get; private set; } = new ReactiveProperty<int>(20);

        //private string createdPassword;
        //public string CreatedPassword
        //{
        //    get => createdPassword;
        //    set => SetProperty(ref createdPassword, value);
        //}
        public ReactiveProperty<string> CreatePassword { get; private set; } = new ReactiveProperty<string>("Prism+ReactiveProperty WPF App.");

        //public DelegateCommand Generate { get; private set; }
        public ReactiveCommand Generate { get; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModelWithReactiveProperty()
        {
            this.Title.AddTo(_cd);
            this.NumOfLetters.AddTo(_cd);
            this.CreatePassword.AddTo(_cd);
            this.IsNonMark.AddTo(_cd);
            IsNonMark.Subscribe(_ => SetFactory());
            Generate = NumOfLetters.Select(x => x > 10).ToReactiveCommand();
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
