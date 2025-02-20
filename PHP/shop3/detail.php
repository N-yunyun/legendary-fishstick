<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<?php
$pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
$sql=$pdo->prepare('select * from product where id=?');
$sql->execute([$_REQUEST['id']]);
foreach($sql as $row){
    echo '<p><img alt="image" src="image/',$row['id'],'.jpg"></p>';
    echo '<form action="cart-insert.php" method="post">';
    echo '<p>商品番号:', $row['id'], '</p>';
    echo '<p>商品名:', $row['name'], '</p>';
    echo '<p>ジャンル:', $row['genre'], '</p>';
    echo '<p>価格:', $row['price'], '</p>';
    echo '<p>在庫数:', $row['stock'], '</p>';
    echo '<p>個数:<select name="count">';
    for($i=1; $i<=10; $i++)
    {
        echo '<option value"', $i, '">', $i, '</option>';
    }
    echo '</select></p>';
    echo '<input type="hidden" name="id" value="', $row['id'],'">';
    echo '<input type="hidden" name="name" value="', $row['name'],'">';
    echo '<input type="hidden" name="price" value="', $row['genre'],'">';
    echo '<input type="hidden" name="price" value="', $row['price'],'">';
    echo '<input type="hidden" name="price" value="', $row['stock'],'">';
    echo '</form>';
    
}
?>
<?php require '../footer.php'; ?>