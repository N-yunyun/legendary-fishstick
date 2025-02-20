<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<form action="product.php" method="post">
    商品検索<br>
    検索タイプを選択してください。<br>
    <p><input type="radio" name="search" value="社員ID検索" <?php if (isset($_POST['search']) && $_POST['search'] == "社員ID検索") echo 'checked'; ?>>社員ID検索</p>
    <p><input type="radio" name="search" value="社員名検索"
              <?php
if (isset($_POST['search']) && $_POST['search'] == "社員名検索" || !isset($_POST['search']))
{
    
echo 'checked';

} ?>>社員名検索</p>
<p><input type="radio" name="search" value="役職検索" <?php if (isset($_POST['search']) && $_POST['search'] == "役職検索") echo 'checked'; ?>>役職検索</p>
<br>
<input type="text" name="keyword">
    <input type="submit" value="検索">
</form>
<hr>
<?php
echo '<table>';
echo '<tr><th>社員ID</th><th>社員名</th><th>年齢</th><th>役職</th><th>住所</th></tr>';
    $pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
    
if(isset($_REQUEST['keyword']))
{
    $sql=$pdo->prepare('select * from product where name like ?');
    switch($_REQUEST['search'])
    {
        case "社員ID検索":
                $sql->execute(['%'.$_REQUEST['keyword'].'%']);
            break;
        case "社員名検索":
            $sql->execute([$_REQUEST['keyword']]);
            break;
        case "役職検索":
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