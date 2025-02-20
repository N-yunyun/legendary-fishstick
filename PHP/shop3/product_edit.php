<?php require '../header2.php'; ?>
<?php require 'menu.php'; ?>
<table>
<div class="th0">商品番号</div>
<div class="th1">商品名</div>
<div class="th1">ジャンル</div>
<div class="th1">価格</div>
<div class="th1">在庫数</div><br>

<?php
$insertOrUpdateOrDelete="";
$pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
$genreList=[
    '実','種','その他',
    ];
// 関数定義
if (!function_exists('array_key_first')) {
    function array_key_first(array $arr) {
        foreach($arr as $key => $unused) {
            return $key;
        }
        return NULL;
    }
}
if (!function_exists("array_key_last")) {
    function array_key_last(array $arr) {
        if (empty($arr)) {
            return NULL;
        }

        return array_keys($arr)[count($arr) - 1];
    }
}
if(isset($_REQUEST['command'])){
switch($_REQUEST['command']){
    case 'insert':
//        echo "insertにはいるよ";
        if(empty($_REQUEST['name'])/* || empty($_REQUEST['genre']) ||
           empty($_REQUEST['price']) || empty($_REQUEST['stock'])*//* || is_numeric($_REQUEST['name'] || !preg_match('/^[ぁ-ゞ|ァ-ヾ]+$/u', $_REQUEST['name'])) || !preg_match('/^[0-9]+$/', $_REQUEST['price']) || !preg_match('/^[0-9]+$/', $_REQUEST['stock'])*/)
        {
            
            echo "追加できませんでした";
//            echo '$_REQUESTの中身は',$_REQUEST['name'],$_REQUEST['genre'],$_REQUEST['price'],$_REQUEST['stock'];
            break;
        }
            echo "追加しました";
//            echo '$_REQUESTの中身は',$_REQUEST['name'],$_REQUEST['genre'],$_REQUEST['price'],$_REQUEST['stock'];
        $sql=$pdo->prepare('insert into product values(null, ?,?,?,?)'); 
        $sql->execute([htmlspecialchars($_REQUEST['name']),$_REQUEST['newGenre'],$_REQUEST['price'],$_REQUEST['stock']]);
        echo $sql->debugDumpParams();
        break;
    case 'update':
        echo "updateにはいるよ";
        if(empty($_REQUEST['name']) || empty($_REQUEST['genre']) ||
           empty($_REQUEST['price']) || empty($_REQUEST['stock']) /*|| is_numeric($_REQUEST['name'] || !preg_match('/^[ぁ-ゞ|ァ-ヾ]+$/u', $_REQUEST['name'])) || !preg_match('/^[0-9]+$/', $_REQUEST['price']) || !preg_match('/^[0-9]+$/', $_REQUEST['stock'])*/)
        {
            echo '更新できませんでした';
            break;
        }
        else{
            
        $sql=$pdo->prepare('update product set name=?, genre=?, price=?, stock=? where id=?'); 
        $sql->execute([htmlspecialchars($_REQUEST['name']),$_REQUEST['genre'],$_REQUEST['price'],$_REQUEST['stock'],$_REQUEST['id']]);
        echo $sql->debugDumpParams();
        break;
        }
        
    case 'delete':
        echo 'deleteにはいるよ';
        $sql=$pdo->prepare('delete from product where id=?'); 
        $sql->execute([$_REQUEST['id']]);
        /*echo '$_REQUESTの中身は',$_REQUEST['name'],$_REQUEST['genre'],$_REQUEST['price'],$_REQUEST['stock'];*/
        break;
    }
}
 echo 'Case文抜けたので次foreach文に入りたい';
foreach($pdo->query('select * from product') as $row)
{
    echo 'foreach文に入った';
    echo '<form class="ib" action="product_edit.php" method="post">'; 
    echo '<input type="hidden" name="command" value="update">';
    echo '<input type="hidden" name="id" value="', $row['id'],'">';
    
    echo '<div class="td0">';
    echo $row['id'];
    echo '</div>';
    
    echo '<div class="td1">';
    
    echo '<input type="text" name="name" value="',$row['name'],'">';
    
    echo '</div>';
    
    echo '<div class="td1">';
    echo '<input type="text" name="genre" value="',$row['genre'],'">';
    ?>
    <select name="genre">
    <?php

foreach($genreList as $value){
    if($value ===$row['genre'])
    {
        echo '<option value="" selected>', $value, '</option>';
    }
    else
    {
        echo '<option value="', $value, '">', $value, '</option>';
    }
    
}
    ?>
</select>
    <?php
    
    echo '</div>';
    
    echo '<div class="td1">';
    
    echo '<input type="text" name="price" value="', $row['price'],'">';
    
    echo '</div>';
    
    echo '<div class="td1">';
    
    echo '<input type="text" name="stock" value="', $row['stock'],'">';
    
    echo '</div>';
    echo '<input type="hidden" name="insertOrUpdateOrDelete" value="次は自分自身を呼び出してコマンドをupdateにする">';
    echo '<div class="td2"><input type="submit" value="更新">';
    
    echo '</div>';
    
    echo '</form>';
    
    echo '<form class="ib" action="product_edit.php" method="post">';
    echo '<input type="hidden" name="command" value="delete">';
    echo '<input type="hidden" name="id" value="', $row['id'],'">';
//    echo '<input type="hidden" name="insertOrUpdateOrDelete" value="次は自分自身を呼び出してコマンドをdeleteにする">';
    echo '<input type="submit" value="削除">';
//    submitボタンを押すとcommandがdeleteになる
    echo '</form><br>';
    echo "\n";
    
}
    
    
//    echo implode(',', $_REQUEST);
//    echo implode(',', array_keys($_REQUEST));
    echo '次は自分自身を呼び出してコマンドをinsertにする';
?>

<form action="product_edit.php" method="post">
<input type="hidden" name="command" value="insert">
<!--submitボタンを押すとcommandがinsertになる-->
<div class="td0"></div>
<div class="td1"><input type="text" name="name"></div>
<div class="td1">
    
<select name="newGenre">
<?php
foreach($genreList as $index => $value){
//    echo '$valueの中身は',$value;
    if($index ===array_key_first($genreList))
    {
        echo '<option value="', $value, '" selected>', $value, '</option>';
        
    }
    else
    {
        echo '<option value="', $value, '">', $value, '</option>';
    }
    
    }
  echo   $_REQUEST['newGenre'];
?>
</select>
</div>
<div class="td1"><input type="text" name="price"></div>
<div class="td1"><input type="text" name="stock"></div>
<input type="hidden" name="insertOrUpdateOrDelete" value="次は自分自身を呼び出してコマンドをinsertにする">
<div class="td2"><input type="submit" value="追加"></div>
    
</form>
<!--
   <?php
    echo $_REQUEST['command'];
    ?>
-->
    </table>
<?php require '../footer.php'; ?>