
var layoutVM = null;
var layoutViewModel = function () {
    layoutVM = this;
    
    evalViewport(); // This is contained in the master page _layout to hold a .js version of bootraps's sizing in use. is set the javascript var 'viewport'
    layoutVM.viewport = ko.observable(viewport); // adjusts later using window.onresize
    layoutVM.contextMenuType = ko.observable('contextMain');
    layoutVM.breadCrumbs = ko.observableArray([]);
    layoutVM.currentUser = ko.observable({})
    layoutVM.mainContainerType = ko.observable('home');

    layoutVM.selectMenuItemNumber = ko.observable(0);
    layoutVM.lastCall_setActiveItem = 0;


    layoutVM.togglePlus = function () {
        $(event.target).toggleClass('fa-plus-square-o fa-minus-square-o');
    }

    layoutVM.soporteMenuitems = ko.observableArray([
        { name: 'Usuario' },
        { name: 'Horario' },
        { name: 'Modulo' },
        { name: 'Curso' },
        { name: 'Programacion' }
    ]);

    layoutVM.procesoMenuitems = ko.observableArray([
        { name: 'Registro Matricula' },
        { name: 'Calificaciones' }
    ]);

    layoutVM.horarioMenuitems = ko.observableArray([
        { name: 'Horario por Curso' },
        { name: 'Horario por Aula' }
    ]);

    layoutVM.pagoMenuitems = ko.observableArray([
        { name: 'Compromiso de Pago' },
        { name: 'Deudas Obtenidas' }
    ]);

    layoutVM.menuClick = function (data, event) {
        if (data.name == "Usuario") {
            inboxPaf();
        }
        else if (data.name == "Horario") {
            $('#mainContainer').load("/Home/Index/", function () {
                window.history.pushState(null,'Page title','/Home')
            });
        }
        else if (data.name == "Registro Matricula") {
            $('#mainContainer').load("/Usuario/Index")

        }
    };

}


function setActiveItem(target) {

    var now = new Date();
    var lastCallTime = layoutVM.lastCall_setActiveItem;
    if (lastCallTime != null && ((now - layoutVM.lastCall_setActiveItem) / 2000) < 1) return;
    layoutVM.lastCall_setActiveItem = now;

    layoutVM.selectMenuItemNumber(target);
    return true;
}



window.onresize = function () {
    // the core variable 'viewport' is contained in the _Layout.cshtml master page.
    // but the onresize event must in a file that can pass to the ViewModel
    // this code updates the observable so that dependent layout templates will upate
    evalViewport();
    layoutVM.viewport(viewport); // Will set the observable with one of these text values: xs, sm, md, lg

    //if (layoutVM.viewport() != 'xxs' && layoutVM.viewport() != 'xs') {
    //    layoutVM.vMenuIn();
    //}
    //if (layoutVM.viewport() == 'xxs' || layoutVM.viewport() == 'xs') {
    //    layoutVM.vMenuOut();
    //}
    //if (layoutVM.viewport() != 'xxs' && layoutVM.viewport() != 'xs') {
    //    $('[data-toggle=tooltip]').tooltip('enable'); //Enable tooltips
    //}
    //if ((layoutVM.viewport() == 'xxs' || layoutVM.viewport() == 'xs')) {
    //    $('[data-toggle=tooltip]').tooltip('disable'); // Disable tooltips

    //}

}

var viewport = null;
function evalViewport() {
    viewport = $(".viewport:visible").html(); // find the visible span based on native bootstrap classes
    $("#viewport").html(viewport);
    $("#jqWidth").html($(window).width());
    return;
}

function inboxPaf() {
    $("#mainContainer").load('/Usuario/Index/');
    $('body').css('background', 'none');
    return true;
}