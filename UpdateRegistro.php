<?php

include "conexao.php";

// pega os dado em forma de post
$json = file_get_contents('php://input');

//transforma em array associativo
$array = json_decode($json, true);

    $sql=" UPDATE TB_USUARIO " .
         " SET nome_us = '".$array['nome_us']."' , " .
         "    email_us  = '".$array['email_us']."' , " .
         "    senha_us  = '".$array['senha_us']."'  " .
         "  WHERE ID_US = ".$array['id_us'];

      //echo $sql;	
  //$resp = array( 0 =>'certo', 1=>'erro');

 
  if (mysqli_query($con,$sql)){
    $array = array('resp'=>'yes');
    echo json_encode($array,true);
  
  }else{
    $array = array('resp'=>'no');
    echo json_encode($array,true);
  }

    

    
$con->close();


?>