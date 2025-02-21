<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<?php
$pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
$sql=$pdo->prepare('insert into product values(null, ?,?,?,?)'); 
if(empty($_REQUEST['name']))
{
    echo '商品名は入力必須事項です。';
    
}
else if(is_numeric($_REQUEST['name'] || preg_match('/^[a-zA-Z]+$/', $_REQUEST['name'])))
{
        
        echo '商品名は数字、英字以外の文字を使用してください';
}

else if(empty($_REQUEST['genre']))
{
    echo 'ジャンルは入力必須事項です。';
    
}
else if(empty($_REQUEST['price']))
{
       echo '価格は入力必須事項です。';
}
else if(preg_match('/^[a-zA-Z]+$/', $_REQUEST['price']))
{
       echo '価格は数字で入力してください。';
}
else if(empty($_REQUEST['stock']))
{
       echo '在庫数は入力必須事項です。';
}
else if(preg_match('/^[a-zA-Z]+$/', $_REQUEST['stock']))
{
       echo '在庫数は数字で入力してください。';
}
else if($sql->execute([htmlspecialchars($_REQUEST['name']),$_REQUEST['genre'],$_REQUEST['price'],$_REQUEST['stock']]))
        {
            echo '商品を登録しました。';
        }

?>
<?php require '../footer.php'; ?> 