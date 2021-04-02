
/**
 *          La vida con vue es mejor que jquery
 * 
*/
var app = new Vue({
    el: '#app',
    data: {
        modaltitulo: '',
        jugadores: {},
        equipos: {},
        equipo: {},
        model: {},
        guardar: false
    },
    methods:{
        cargaJugadores: function(){
            axios.get('/api/jugadores')
            .then(function(response){
                app.jugadores = response.data;
            });
        },
        cargaEquipos: function () {
            axios.get('/api/equipos')
            .then(function(response){
                app.equipos = response.data;
            }); 
        },
        buscaEquipo: function(id){
            for(var i = 0;i<this.equipos.length;i++){
                if(this.equipos[i].equipoId===id)
                    return this.equipos[i].descripcion;
            }
        },
        editar: function(m){
            this.model = m;
            this.guardar = false;
            this.modaltitulo = 'Editar';
            this.model.nacimiento = m.nacimiento.substr(0,10);
            $('#modalJugador').modal('show');
        },
        nuevo: function(){
            this.model = {
                nombre: '',
                nacimiento: '',
                descripcion: ''
            }
            this.guardar = true;
            this.modaltitulo = 'Nuevo';
            $('#modalJugador').modal('show');
        },
        guardarJugador: function(){
            this.cambiarTexto();
            if(this.guardar){
                //Guardar
                axios.post('/api/jugadores',app.model).then(function(response){
                    app.cargaJugadores();
                });
            }else{
                //Actualizar
                axios.put('/api/jugadores/'+app.model.jugadorId,app.model).then(function(response){
                    app.cargaJugadores();
                });
            }
            this.cerrarModal();
        },
        cerrarModal: function(){
            $('#modalJugador').modal('hide');
            this.cargaJugadores();
        },
        cambiarTexto: function(e){
            this.model.descripcion = this.removerAcentos(this.model.descripcion);
        },
        removerAcentos: function(str){
            return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
        }
    },
    created(){
        this.cargaEquipos();
        this.cargaJugadores();
    }
});