<?php require '../header.php'; ?>
<?php
$file='board.txt';
    $_REQUEST['message'];
if(file_exists($file)){
    
$board=json_decode(file_get_contents($file));
}
$board[]=htmlspecialchars($_REQUEST['message']);
file_put_contents($file,json_encode($board));
foreach($board as $message)
{
    echo '<p>', $message, '</p><hr>';
}
?>
<?php require '../footer.php'; ?>