2023年工陵祭で展示したVRゲーム  
初期案：ハードウェアとVR機器を組み合わせたゲームを作成  
・景色の良いジップライン体験  
　実際に吊り下げて加速度センサからデータを取得、ゲーム内と動きを同期  
　扇風機等をマイコン制御し、風を送って没入感向上  
  
・実際にモデルガンを操作して行うシューティングゲーム  
　引き金の入力などをゲームに反映  
　実際に構えることでより没入感の高い狙いづけ  
  
体験者の酔い防止のためプレイヤーが大きく動かずに済む、シューティングゲームを採用した。  

練習場  
弾道に重力や風の影響を反映  
実際に弾道が落下している。スコープ左側の数字はゼロイン距離であり、この例では150m先でレティクル通りにヒットする
![1](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/55b54fad-1e88-4fce-b984-d35a834e8701)

スコープで距離の計測、ゼロイン距離の変更が可能  
風向きはパーティクルで提示
![2](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/1e1ffbf9-5ae0-44c7-8b9c-36356156bc79)
![3](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/a7f3bb2c-c6bf-42fd-a336-6f3f0c4052b5)
最終的には鍵の破壊など、ミッションをクリアする形式にしたかったが  
期間と展示環境の都合上断念。スコアアタック形式とした。  
![PB050225](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/126ae077-1c7d-4b92-b06e-cf5daf7878b0)

理想はマテリアルスナイパー ( https://nextframe.jp/flash/matsnp/matsnp.html )をイメージ
![4](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/4516f302-b985-4d24-9460-2f49418b2cb8)

実際のコントローラ  
　esp32DevKitCを使用  
　Bluetoothシリアル通信で入力状況を伝送  
　測距、スコープ付け替え、スコープ倍率変更をボタンに割り当てた。  
　実際に引き金を引いて射撃する。  
　トラッキングはVRコントローラのものを使い、モデルガン(動作しないエアソフトガン)に固定して使用した。
![5](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/9d7ed5ed-3a45-4eaa-9efc-67095dffa153)
![6](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/4eaba88c-d93a-4aac-b16f-636916f2ab7f)
![7](https://github.com/kino-n1851/fps_vr_koryo2023/assets/46987400/060ca191-ee8f-4e1f-8f40-6be48ee0a94f)




