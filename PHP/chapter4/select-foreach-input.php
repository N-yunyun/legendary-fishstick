<?php require '../header.php'; ?>
    <p>購入数を選択してください。</p>
<form action="select-foreach-output.php" method="post">
<select name="color">
    <?php
    $color=['ホワイト','ブルー','レッド','イエロー','ブラック'];
foreach ($color as $c) {echo '<option value="',$c,'">',$c,'</option>';
        }
    ?>
    </select>
     <p><input type="submit" name="確定"></p>
</form>
<?php require '../footer.php'; ?>