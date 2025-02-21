<?php require '../header2.php'; ?>
<?php require 'menu.php'; ?>
<table>
<div class="th0">社員番号</div>
<div class="th1">社員名</div>
<div class="th1">年齢</div>
<div class="th1">役職</div>
<div class="th1">住所</div>
<br>

<?php
$pdo=new PDO('mysql:host=localhost;dbname=shop3;charset=utf8','staff','password');
$genreList=[
    '一般社員','秘書','主任','係長','課長','部長','専務'
    ];
if(isset($_REQUEST['command'])){
switch($_REQUEST['command']){
    case 'insert':
        echo "insertにはいるよ";
        if(empty($_REQUEST['name']) || empty($_REQUEST['age']) || empty($_REQUEST['post']) ||
            empty($_REQUEST['address']) || is_numeric($_REQUEST['name'] || !preg_match('/^[ぁ-ゞ|ァ-ヾ]+$/u', $_REQUEST['name'])) || !preg_match('/^[0-9]+$/', $_REQUEST['age']) || !preg_match('/^[0-9]+$/', $_REQUEST['age']))
        {
            break;
        }
        $sql=$pdo->prepare('insert into employee values(null, ?,?,?,?)');
        $sql->execute([htmlspecialchars($_REQUEST['name']),$_REQUEST['age'],$_REQUEST['post'],$_REQUEST['address']]);
        break;
    case 'update':
        echo "updateにはいるよ";
        if(empty($_REQUEST['name']) || empty($_REQUEST['age']) || empty($_REQUEST['post']) ||
            empty($_REQUEST['address']) || is_numeric($_REQUEST['name'] || !preg_match('/^[ぁ-ゞ|ァ-ヾ]+$/u', $_REQUEST['name'])) || !preg_match('/^[0-9]+$/', $_REQUEST['age']) || !preg_match('/^[0-9]+$/', $_REQUEST['age']))
        {
            echo "更新できませんでした";
            break;
        }
        $sql=$pdo->prepare('update employee set name=?, genre=?, price=?, stock=? where id=?'); 
        $sql->execute([htmlspecialchars($_REQUEST['name']),$_REQUEST['age'],$_REQUEST['post'],$_REQUEST['address']]);
        echo $sql->debugDumpParams();
        break;
    case 'delete':
        echo "deleteにはいるよ";
        $sql=$pdo->prepare('delete from employee where id=?'); 
        $sql->execute([$_REQUEST['id']]);
        break;
    }
}
foreach($pdo->query('select * from employee') as $row)
{
    echo '<form class="ib" action="employee_edit.php" method="post">'; 
    echo '<input type="hidden" name="command" value="update">';
    echo '<input type="hidden" name="id" value="', $row['id'],'">';
    
    echo '<div class="td0">';
    echo $row['id'];
    echo '</div>';
    
    echo '<div class="td1">';
    
    echo '<input type="text" name="name" value="',$row['name'],'">';
    
    echo '</div>';
    
    echo '<div class="td1">';
    
    echo '<input type="text" name="age" value="', $row['age'],'">';
    
    echo '</div>';
    
    echo '<div class="td1">';
    ?>
    <select name="post">
    <?php

foreach($genreList as $value){
    if($value ===$row['post'])
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
    
    echo '<input type="text" name="address" value="', $row['address'],'">';
    
    echo '</div>';
    
    echo '<div class="td2"><input type="submit" value="更新">';
    
    echo '</div>';
    
    echo '</form>';
    
    echo '<form class="ib" action="employee_edit.php" method="post">';
    echo '<input type="hidden" name="command" value="delete">';
    echo '<input type="hidden" name="id" value="', $row['id'],'">';
    echo '<input type="submit" value="削除">';
    echo '</form><br>';
    echo "\n";
    
}
    echo implode(',', $_REQUEST);
    echo implode(',', array_keys($_REQUEST));
?>
<form action="employee_edit.php" method="post">
<input type="hidden" name="command" value="insert">
<div class="td0"></div>
<div class="td1"><input type="text" name="name"></div>
<div class="td1">
<select name="newGenre">
<?php
foreach($genreList as $value){
    
        echo '<option value="', $value, '">', $value, '</option>';
    }
?>
</select>
</div>
<div class="td1"><input type="text" name="price"></div>
<div class="td1"><input type="text" name="stock"></div>
    
<div class="td2"><input type="submit" value="追加"></div>
</form>
    </table>
<?php require '../footer.php'; ?>