# 双方向連結リスト(Bidirectional LinkedList)

---

## 概要



[未確認飛行C](https://ufcpp.net/study/algorithm/col_blist.html)  

---

## new LinkedList()した時の状態

``` txt
DummyNode
prev  val  next
Dummy null Dummy
```

---

## InsertAfterの動作

``` C#
    /// <summary>
    /// ノード n の後ろに新しい要素を追加。
    /// </summary>
    /// <param name="n">現在のノード(要素の挿入位置)</param>
    /// <param name="elem">新しい要素</param>
    /// <returns>新しく挿入されたノード</returns>
    public Node InsertAfter(Node n, T elem)
    {
        // 前のポインタが現在のノードを指し示し、次のポインタが現在のノードの次のポインタを指し示すノードを作る
        Node m = new Node(elem, n, n.Next);
        // 現在のノードの次のノード。そのノードの前のノードに作成したノードを登録する。
        n.Next.Previous = m;
        // 現在のノードの次のノードに作成したノードを登録する。
        n.Next = m;
        // 作成したノードを返却する。
        return m;
    }
```

現在のノード状況が以下のような場合にnをダミーノードとし、InsertAfterされた状況を図解する。

``` txt
DummyNode        | Node1
prev  val  next  | prev  val   next
Node1 null Node1 | Dummy Node1 Dummy
```

①`Node m = new Node(elem, n, n.Next);`  
①を実行する事で前のノードがDummy、次のノードがNode1を指し示すノード:Node2を作成する。  

``` txt
Node2
prev  val   next
Dummy Node2 Node1
```

②`n.Next.Previous = m;`  
nはダミーノード。n.NextはNode1を指し示している。  
n.Next.PreviousはNode1のPreviousを参照することを意味し、Node1.PreviousはDummyNodeを参照しているので、ここを今回作成したNode2を参照するようにする。  

``` txt
DummyNode        | Node1
prev  val  next  | prev  val   next
Node1 null Node1 | Node2 Node1 Dummy
                    ↑
```

③`n.Next = m;`  
nはダミーノード。n.NextはNode1を指し示しているので、参照をNode2とする。  

``` txt
DummyNode        | Node1
prev  val  next  | prev  val   next
Node1 null Node2 | Node2 Node1 Dummy
            ↑
```

ここまでを実行した結果、以下のような状態となる。  

``` txt
DummyNode        | Node2             | Node1
prev  val  next  | prev  val   next  | prev  val   next
Node1 null Node2 | Dummy Node2 Node1 | Node2 Node1 Dummy
```

---

## InsertBeforeの動作

``` C#
    /// <summary>
    /// ノード n の前に新しい要素を追加。
    /// </summary>
    /// <param name="n">現在のノード(要素の挿入位置)</param>
    /// <param name="elem">新しい要素</param>
    /// <returns>新しく挿入されたノード</returns>
    public Node InsertBefore(Node n, T elem)
    {
        // ①前のポインタが現在のノードの前のノードを指し示し、次のポインタが現在のノードを指し示すノードを作る
        Node m = new Node(elem, n.Previous, n);
        // ②現在のノードの前のノード。そのノードの次のノードに作成したノードを登録する。
        n.Previous.Next = m;
        // ③現在のノードの前のノードに作成したノードを登録する。
        n.Previous = m;
        // 作成したノードを返却する。
        return m;
    }
```

現在のノード状況が以下のような場合にnをダミーノードとし、InsertBeforeされた状況を図解する。

``` txt
Node1             | DummyNode
prev  val   next  | prev  val  next
Dummy Node1 Dummy | Node1 null Node1
```

①`Node m = new Node(elem, n.Previous, n);`  
①を実行する事で前のノードがNode1、次のノードがDummyを指し示すノード:Node2を作成する。  

``` txt
Node2
prev  val   next
Node1 Node2 Dummy
```

②`n.Previous.Next = m;`  
nはダミーノード。n.PreviousはNode1を指し示している。  
n.Previous.NextはNode1のNextを参照することを意味し、Node1.NextはDummyNodeを参照しているので、ここを今回作成したNode2を参照するようにする。  

``` txt
Node1             | DummyNode
prev  val   next  | prev  val  next
Dummy Node1 Node2 | Node1 null Node1
             ↑
```

③`n.Previous = m;`  
nはダミーノード。n.PreviousはNode1を指し示しているので、参照をNode2とする。  

``` txt
Node1             | DummyNode
prev  val   next  | prev  val  next
Dummy Node1 Node2 | Node2 null Node1
                     ↑
```

ここまでを実行した結果、以下のような状態となる。  

``` txt
Node1             | Node2             | DummyNode
prev  val   next  | prev  val   next  | prev  val  next
Dummy Node1 Node2 | Node1 Node2 Dummy | Node2 null Node1
```
