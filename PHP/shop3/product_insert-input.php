<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<?php
$name=$genre=$price=$stock='';
if(isset($_SESSION['product'])){
$name=$_SESSION['product']['name'];
    $genre=$_SESSION['product']['genre'];
        $price=$_SESSION['product']['price'];
        $stock=$_SESSION['product']['stock'];
}
    echo '<form action="product-output.php" method="post">';
    echo '<table>';
    echo '<tr><td>商品名</td><td>';
    echo '<input type="text" name="name" value="', $name, '">';
    echo '</td></tr>';
    echo '<tr><td>ジャンル</td><td>';
?>
<select name="genre">
    <?php
    $genreList=[
    '実','種','その他',
    ];

foreach($genreList as $value){
    
    echo '<option value="', $value, '">', $value, '</option>';
}
    ?>
</select>
<?php
    echo '</td></tr>';
    echo '<tr><td>価格</td><td>';
    echo '<input type="text" name="price" value="', $price, '">';
    echo '</td></tr>';
    echo '<tr><td>在庫</td><td>';
    echo '<input type="password" name="stock" value="', $stock, '">';
    echo '</td></tr>';
    echo '</table>';
    echo '<input type="submit" value="確定">';
    echo '</form>';
?>
<?php require '../footer.php'; ?> 