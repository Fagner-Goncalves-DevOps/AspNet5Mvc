# AspNet5Mvc
NET5 Mvc essentials
-- Crud Basico melhoria bootstrap
-- parei 8 -  236 uploads
-- Projeto completo de estudo, com diversas final
-- dropdown e ultimos detalhes
-- ajustes finais



//modelo processo
	DropDownList1.Attributes.Add("onclick", "return disableItem('" +DropDownList1.ClientID+ "')");
	function disableItem(ddl) { 
		var dropObj = document.getElementById(ddl); 
			dropObj[2].disabled = true; }

	
  
//to do do modelo como seria, pensar amanha  
	//loop para pegar 1 por 1
	//pegar carga dos value que vem na conrtoller como desativados
	//salvar ele em variaveis num
	//chama function e passa num
  
	function disableItem(num) {
			
		// fazer assim passar os ids por varivel que estão desativados 
		//desativar somente eles
		document.querySelector(' input[value="'+num+'"]').setAttribute('disabled',true);
	}
	
