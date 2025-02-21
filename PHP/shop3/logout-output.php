<?php session_start();?>
<?php require '../header3.php'; ?>
<?php require 'menu.php'; ?>
<?php
if(isset($_SESSION['customer'])){
unset($_SESSION['customer']);
echo 'ログアウトしました';
}
else{
echo '既にログアウトしています。';
}
?>
<?php require '../footer.php'; ?> 