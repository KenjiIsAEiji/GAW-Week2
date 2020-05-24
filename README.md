# GAW-Week2
5/18～5/24までのGAWリポジトリ

### 制作1日目　制作時間 5時間 アイディア出し
　今回は初めにじっくりとブレインストーミングして、自分が今までにやったことがなかった、マリオみたいな横スクロール3Dアクションを作ろうと決めました。初めは格闘系で二段ジャンプやジェットパックをつけたタイプを考えましたが、「APEX」で瞬間移動するキャラが最近出たことを思い出して、ビーコンを投げてそのビーコンに瞬間移動して戦うゲームを思いついたので、今回は横スクロール3D格闘＋瞬間移動なゲームを作ってみようかと思います。
 そのことを決めたうえで、今日はアイディアが出るまでかなり時間を使ったので、ざっくりとシステムを決めてから明日以降詳しく決めてから、作り始めたいと思います。

<img src="https://user-images.githubusercontent.com/41467408/82228588-4284cc00-9964-11ea-83af-b9f2ea16e491.jpeg" width="320px">

### 制作2日目　制作時間 5時間　プレイヤーの移動と走るアニメーションなど
　キーボードのA・Dキーで左右に走り、Wキーでジャンプできるようにしました。プレイヤーの移動は、抵抗力があるため適度な速度に収束できるように与える力を変えるようにプログラムしています。また、速度や接地判定に応じてアニメーションが変化するようにアニメーター使用。

<img src="https://user-images.githubusercontent.com/41467408/82337184-71637680-9a26-11ea-8a13-1517ba547c06.png" width="640px">

### 制作3日目　制作時間 5時間　プレイヤーの攻撃と当たり判定
　マウスクリックでプレイヤーが攻撃アニメーションを行い、長押しすることでコンボするようにしました。アニメーションイベントを使って攻撃を行う瞬間に、周囲のリジッドボディがついているオブジェクトを取得して、前方に力を与えて吹き飛ばすようにしています。範囲はボックス状にしており、ボックス内に敵もしくは吹き飛ばせるオブジェクトが入っている状態で、アニメーションイベントが呼ばれると攻撃がヒットするように実装しました。

<img src="https://user-images.githubusercontent.com/41467408/82457168-14cc8e00-9af0-11ea-8b62-37dd555b4719.jpg" width="500px">

<img src="https://user-images.githubusercontent.com/41467408/82457183-18f8ab80-9af0-11ea-8355-371bf6a4c30c.jpg" width="320px">

### 制作4日目　制作時間 6時間　敵モデルの決定と空中攻撃
　空中時の別のコンボを設定して、空中にいる間でも攻撃が可能にしました。瞬間移動を高台に移動する目的にしてしまうと、敵をジャンプさせるのも面倒なので、でっかいボス敵の高い位置にある弱点を狙えるようにすることや、回避に瞬間移動を使う目的にしようとを決めて、敵モデルも大きいロボと小さいロボの二つを用意しました。そのためasset吟味に時間がかかってしまったので、アセットストアを漁るのは作業時間外でパパっと済ませようと感じました。

<img src="https://user-images.githubusercontent.com/41467408/82569744-9f2df400-9bbb-11ea-997b-28a274ae2b87.png" width="300px"><img src="https://user-images.githubusercontent.com/41467408/82570245-4b6fda80-9bbc-11ea-936c-5b2fc6485abc.jpg" width="320px">

### 制作5日目　制作時間 10時間　敵の移動に関しての仕様変更とヒットストップ
　最初はナビゲーションを使用して敵を動かしていたが、ノックバックの動きがどうしてもイマイチで、移動は横方向のみに限定しているので、常にプレイヤを向いて一定速度で移動するのをRigidbodyを使って実装しなおしました。それに結構な時間を使って、残りの時間でステージとヒットストップをパパっと作って今日は終了。明日のうちにGameManagerまで実装してしまいたい...
 
<img src="https://user-images.githubusercontent.com/41467408/82679384-df59a900-9c85-11ea-8dc5-67e89204fe36.png" width="500px">

### 制作6日目　制作時間 13時間　敵からの攻撃とHP定義、瞬間移動用ビーコン発射前段階
　敵とプレイヤーそれぞれにHPを設けて、敵はHPが0になると倒れ、プレイヤーはUIの表示がダメージによって変化するようになりました。プレイヤーも攻撃を受けるとノックバックをするアニメーションを追加し、攻撃のコンボが解除されるようになっています。また、瞬間移動移動先のビーコンを右クリックで発射するために、マウスの方向を示すポインタを用意したり、発射位置を決めたりして最終日に畳み込む形に。

<img src="https://user-images.githubusercontent.com/41467408/82732887-2bbaec80-9d4b-11ea-92af-d7340093c024.png" width="500px">
