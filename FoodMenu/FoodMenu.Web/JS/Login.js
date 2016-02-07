$(function () {

    $(document).on('click', "#register", function () {
        common.showModel("/home/part", {}, "title temp", function () {
            bootbox.alert("alert");
        });
    });
        
   

});

            
	