<?php require '../header.php'; ?>
<?php

if(is_uploaded_file($_FILES['file']['tmp_name']))
{
    if(!file_exists('upload'))
    {
        mkdir('upload');
    }   
}
$file='upload/'.basename($_FILES['file']['name']);
if(move_uploaded_file($_FILES['file']['tmp_name'],$file))
   {
       echo $file,'のアップロードに成功しました。';
       echo '<p><img alt="image" src="',$file, '"></p>';
   }
else
{
    
echo 'ファイルを選択してください。';
}

?>
<?php require '../footer.php'; ?>