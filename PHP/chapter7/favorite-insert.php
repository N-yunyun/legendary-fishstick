<?php session_start();?>
<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<?php
if(isset($_SESSION['customer'])){
    $pdo=new PDO('mysql:host=localhost;dbname=shop2;charset=utf8','staff','password');
    $sql=$pdo->prepare('insert into favorite values(?,?)');
    
$sql->execute([$_SESSION['customer']['id'],$_REQUEST['id']]);
    
    echo 'お気に入り商品を追加しました。';
    
    echo $_SESSION['customer']['id'],$_REQUEST['id'];
    
    echo '<hr>';
    require 'favorite.php';
}
else
{
    echo 'お気に入りに商品を追加するには、ログインしてください。';
}

?>
<?php require '../footer.php'; ?>