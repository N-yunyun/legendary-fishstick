<?php require '../header.php'; ?>
    <p>購入数を選択してください。</p>
<form action="select-for-output.php" method="post">
    <select name="count">
    <option value="0">0</option>
        <option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="6">6</option><option value="7">7</option><option value="8">8</option><option value="9">9</option>
    </select>
    <p><input type="submit" name="確定"></p>
</form>
<?php require '../footer.php'; ?>