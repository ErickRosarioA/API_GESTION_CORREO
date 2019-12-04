var ViewModel = function () {
    var self = this;
    self.users = ko.observableArray();

    self.newUser = {
        Id: ko.observable(),
        Nombre: ko.observable(),
        Apellido: ko.observable(),
        TipoUser: ko.observable(),
        FechaNacimiento: ko.observable(),
        CorreoElectronico: ko.observable(),
        TipoProveedor: ko.observable(),
        ContraCorreo: ko.observable(),
        ContraApp: ko.observable()
    }
    //self.contacts = ko.observableArray();
    //self.mensajes = ko.observableArray();

    self.error = ko.observable();

    var UsersUri = '/api/Users/';
    //var ContactsUri = '/api/Contacts/';
    //var MensajesUri ='/api/MensajesEnviados'


    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllUsers() {
        ajaxHelper(UsersUri, 'GET').done(function (data) {
            self.users(data);
        });
    }

    self.addUser = function (formElement) {
        var user = {
            Id: self.newUser.Id(),  
            Nombre: self.newUser.Nombre(),
            Apellido: self.newUser.Apellido(),
            tipoUser: self.newUser.tipoUser(),
            FechaNacimiento: self.newUser.FechaNacimiento(),
            CorreoElectronico: self.newUser.CorreoElectronico(),
            TipoProveedor: self.newUser.TipoProveedor(),
            ContraCorreo: self.newUser.ContraCorreo(),
            ContraApp: self.newUser.ContraApp()
        };
        ajaxHelper(UsersUri, 'POST', user).done(function (item) {
            self.users.push(item);
        });
    }
     getAllUsers();

    //function getAllContacts() {
    //    ajaxHelper(ContactsUri, 'GET').done(function (data) {
    //        self.contacts(data);
    //    });
    //}

    //function getAllMensajes() {
    //    ajaxHelper(MensajesUri, 'GET').done(function (data) {
    //        self.mensajes(data);
    //    });
    //}
    //// Fetch the initial data.

    //getAllMensajes();
    //getAllContacts();
};

ko.applyBindings(new ViewModel());