<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<form action="product.php" method="post">
    商品検索<br>
    検索タイプを選択してください。<br>
    <p><input type="radio" name="search" value="あいまい検索" <?php if (isset($_POST['search']) && $_POST['search'] == "あいまい検索") echo 'checked'; ?>>あいまい検索</p>
    <p><input type="radio" name="search" value="完全一致検索"
              <?php
if (isset($_POST['search']) && $_POST['search'] == "完全一致検索" || !isset($_POST['search']))
{
    
echo 'checked';

} ?>>完全一致検索</p>
<br>
<input type="text" name="keyword">
    <input type="submit" value="検索">
</form>
<hr>
<?php
echo '<table>';
echo '<tr><th>商品番号</th><th>商品名</th><th>価格</th></tr>';
    $pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
    
if(isset($_REQUEST['keyword']))
{
    $sql=$pdo->prepare('select * from product where name like ?');
    switch($_REQUEST['search'])
    {
        case "あいまい検索":
                $sql->execute(['%'.$_REQUEST['keyword'].'%']);
            break;
        case "完全一致検索":
            $sql->execute([$_REQUEST['keyword']]);
            break;
    }      
}
    else{
        $sql=$pdo->query('select * from product'); 
    }
foreach($sql as $row){
    $id=$row['id'];
    echo '<tr>';
    echo '<td>',$id, '</td>';
    echo '<td>';
    echo '<a href="detail.php?id=',$id, '">',$row['name'],'</a>';
    echo '</td>';
    echo '<td>', $row['price'], '</td>';
    echo '</tr>';
    }
    echo '</table>';
?>
<?php require '../footer.php'; ?> 