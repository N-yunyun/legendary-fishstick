<?php require '../header.php'; ?>
<p>アップロードするファイルを指定してください。</p>
<form action="upload-output.php" method="post" enctype="multipart/form-data">
<input type="file" name="file">
     <p><input type="submit" name="アップロード"></p>
</form>
<?php require '../footer.php'; ?>