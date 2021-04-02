
(
    function(){
    $.get('/api/equipos',function(data){
        for(var i=0;i< data.length;i++){
            var item = $('<tr><td><a href="javascript:eliminaEquipo('+data.equipoId+
            ')"> <img width="25" src="https://www.flaticon.es/premium-icon/icons/svg/2907/2907762.svg"  ></a></td>'+
            '<td>'+ data[i].equipoId +
            '</td><td><a onclick="editar('+data[i].equipoId+')">'+ data[i].descripcion +
            '</a></td><td>'+ data[i].fundacion +'</td></tr>');

            $('#equiposTable').append(item);
        }
    });


})();


function agregar(){
    $('#frmEquipo')[0].reset();
    $('#modalEquipo').modal('show');
    document.getElementById('modalTitle').innerHTML = "Agregar";
    document.getElementById('Estatus').value = "Guardar";
    
}

function editar(id){
    $('#modalEquipo').modal('show');
    document.getElementById('modalTitle').innerHTML = "Editar";
    document.getElementById('Estatus').value = "Actualizar";
    $.get('/api/equipos/'+id,function(data){
        document.getElementById('Equipo').value = data.descripcion;
        document.getElementById('Fundacion').valueAsDate = new Date(data.fundacion);
        document.getElementById('Logotipo').value = data.logotipo;
        document.getElementById('EquipoId').value = data.equipoId;

    });
}

$('#frmEquipo').submit(function(e){
    e.preventDefault();
    if (document.getElementById('Estatus').value == 'Guardar'){
        data = {
            descripcion: document.getElementById('Equipo').value,
            fundacion: document.getElementById('Fundacion').value,
            logotipo: document.getElementById('Logotipo').value
        };

        axios.post('/api/equipos',data).then(function(response){
            //console.log(response.data);
            var item = $('<tr><td><a href="javascript:eliminaEquipo('+response.data.equipoId+
            ')"> <img width="25" src="https://www.flaticon.es/premium-icon/icons/svg/2907/2907762.svg"  ></a></td>'+
            '<td>'+ response.data.equipoId +
            '</td><td><a onclick="editar('+ response.data.equipoId+')">'+ response.data.descripcion +
            '</a></td><td>'+ response.data.fundacion +'</td></tr>');

            $('#equiposTable').append(item);
            cerrarModal();
        });
        
    }else{
        //Actualizar
        var id = document.getElementById('EquipoId').value;
        data = {
            descripcion: document.getElementById('Equipo').value,
            fundacion: document.getElementById('Fundacion').value,
            logotipo: document.getElementById('Logotipo').value,
            equipoId: id
        };
        axios.put('/api/equipos/'+id,data).then(function(response){
            window.location = 'equipos.html';
        });
    }
})

function cerrarModal()
{
    $('#modalEquipo').modal('hide');

}


function eliminaEquipo(id){
    Swal.fire({
        title: '¿Realmente deseas eliminar el jugador?',
        text: "Eliminar jugador",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminalo'
      }).then((result) => {
        if (result.isConfirmed) {

          Swal.fire(
            'Eliminado!',
            'El registro fue removido correctamente.',
            'success'
          )
        }
      })
}