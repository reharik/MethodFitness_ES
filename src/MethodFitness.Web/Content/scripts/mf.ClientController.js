/**
 * Created by JetBrains RubyMine.
 * User: RHarik
 * Date: 10/6/11
 * Time: 4:52 PM
 * To change this template use File | Settings | File Templates.
 */
if (typeof mf == "undefined") {
            var mf = {};
}


mf.ClientController  = mf.CrudController.extend({
    events:_.extend({
    }, mf.CrudController.prototype.events),

    initialize:function(options){
        $.extend(this,this.defaults());
        mf.contentLevelControllers["ClientController"]=this;
        $.unsubscribeByPrefix("/contentLevel");
        this.id="clientController";
        this.registerSubscriptions();

        var _options = $.extend({},this.options, options);
        _options.el="#masterArea";
        this.views.gridView = new mf.GridView(_options);
    },

    registerAdditionalSubscriptions: function(){
        $.subscribe('/contentLevel/form_mainForm/payment',$.proxy(this.payment,this), this.cid);
    },
    payment:function(url){
        $.address.value(url);
    },
    //from grid
    addEditItem: function(url){
        var formOptions = {
            el: "#detailArea",
            id: "mainForm",
            url: url,
            formViewName:"ClientFormView"
        };
        $("#masterArea","#contentInner").after("<div id='detailArea'/>");
        this.modules.formModule = new mf.FormModule(formOptions);
    }
});
