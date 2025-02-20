<?php require '../header.php'; ?>
<p>投稿するメッセージを入力してください。</p>
<form action="board-output.php" method="post">
<input type="text" name="message">
     <p><input type="submit" name="投稿"></p>
</form>
<?php require '../footer.php'; ?>