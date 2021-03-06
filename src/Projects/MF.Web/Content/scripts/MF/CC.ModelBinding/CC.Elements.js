/**
 * Created by JetBrains RubyMine.
 * User: Owner
 * Date: 7/15/12
 * Time: 10:40 AM
 * To change this template use File | Settings | File Templates.
 */

if (typeof CC == "undefined") {
    var CC = {};
}
CC.Elements={};

CC.Elements.Element = function($container){
     this.$container = $container;
     this.trimFieldName = function(){
        var name = this.$container.find("label").text();
        name = name.replace(":","");
        name = name.replace("*","");
        return $.trim(name);
    }
};
CC.Elements.Element.extend = Backbone.View.extend;
$.extend(CC.Elements.Element.prototype,{
    init:function(view){
        this.viewId = view.cid;
        this.cid = _.uniqueId("c");
        this.$input = this.$container.find("input");
        this.type = "Element";
        this.friendlyName = this.trimFieldName();
        this.name = this.$input.attr('name');
        this.errorSelector = view.errorSelector?view.errorSelector:"#messageContainer";
    },
    validate:function(){
        CC.ValidationRunner.runElement(this,this.errorSelector);
    },
    getValue:function(){
        return this.$input.val();
    },
    setValidState: function(isValid){
        if(isValid){
            this.$input.removeClass("invalid");
            if(!this.$input.hasClass("valid"))
            this.$input.addClass("valid")
        }else{
            this.$input.removeClass("valid");
            if(!this.$input.hasClass("invalid"))
            this.$input.addClass("invalid")
        }
        this.isValid = isValid;
    },
    destroy:function(){
        $.noty.closeByElementId(this.cid);
    }
});

CC.Elements.Textbox = CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "textbox";
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.DateTextbox = CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "datetextbox";
        this.$label = this.$container.find("label");
        this.$input.on("change",function(){that.validate();});
        this.$input.datepicker({ changeYear: true, changeMonth: true });
//        this.$input.scroller(this.dateTimeDefaults());
    },
//    dateTimeDefaults : function(){
//        var headerText = this.$label.is(":visible")?this.friendlyName+" ":'';
//        return {
//            preset: 'date',
//            theme: 'default',
//            display: 'modal',
//            mode: 'scroller',
//            dateOrder: 'mmddyyyy',
//            headerPreText:headerText
//        }
//    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

//CC.Elements.TimeTextbox = CC.Elements.Element.extend({
//    render:function(){
//        var that = this;
//        this.type = "timetextbox";
//        this.$label = this.$container.find("label");
//        this.$input.on("change",function(){that.validate();});
//        this.$input.scroller(this.timeDefaults());
//    },
//    timeDefaults: function(){
//        var headerPreText = this.$label.is(":visible")?this.$label.text()+" ":'';
//        return {
//            preset: 'time',
//            theme: 'default',
//            display: 'modal',
//            mode: 'scroller',
//            dateOrder: 'hh:mm',
//            headerPreText:headerPreText,
//            onClose:function(valText,inst){
//                MF.vent.trigger(this.name+":timeBox:close",[valText,inst]);
//            }
//        }
//    },
//    destroy:function(){
//        this.$input.off("change");
//        this._super("destroy",arguments);
//    }
//});

CC.Elements.NumberTextbox = CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "numbertextbox";
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Textarea = CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "textarea";
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Checkbox = CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "checkbox";
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.Password= CC.Elements.Element.extend({
    render:function(){
        var that = this;
        this.type = "textbox";
        this.$input.on("change",function(){that.validate();});
    },
    destroy:function(){
        this.$input.off("change");
        this._super("destroy",arguments);
    }
});

CC.Elements.FileSubmission = CC.Elements.Element.extend({
    render:function(view){
        this.type = "file";
        this.$input = this.$container.find("#FileUrl");
        if(view.model.FileUrl()){
            this.showImage();
        }else{
            this.showInput();
        }
        this.$container.find(".deleteImage").on("click",$.proxy(function(){
            this.showInput();
            view.model.DeleteImage(true);
        },this));
    },

    showImage:function(){
        this.$container.find(".imageContainer").show();
        this.$container.find(".inputContainer").hide();
    },
    showInput:function(){
        this.$container.find(".imageContainer").hide();
        this.$container.find(".inputContainer").show();
        this.$input.customFileInput();
    }
});

CC.Elements.PictureGallery= CC.Elements.Element.extend({
    render:function(){
        this.type = "ul";
        this.$input = this.$container.find("ul");
        if(this.$input.find("li").size>0){
            this.$input.galleryView({panel_width:500,panel_height:250});
        }
    }
});

CC.Elements.Select = CC.Elements.Element.extend({
    init:function(){
        var that = this;
        this._super("init",arguments);
        this.type = "select";
        this.$input = this.$container.find("select");
        this.name = this.$input.attr('name');
        this.$input.on("change",function(){that.validate();});
    },
    render:function(){
        this.type = "select";
        this.$input = this.$container.find("select");
//        this.$input.select2();
    },
    destroy:function(){
//        this.$input.select2("destroy");
        this._super("destroy",arguments);
    }
});

CC.Elements.MultiSelect = CC.Elements.Element.extend({
    init:function(view){
        this._super("init",arguments);
        if($.isFunction(view.multiSelectModifier)){
            view.multiSelectModifier(this);
        }
    },
    render:function(options){
        var that = this;
        this.type = "select";
        this.$input = this.$container.find("input.multiSelect");
        this.item = this.$input.data("tokenInputObject");
        this.item.render(this.multiSelectOptions);
        MF.vent.bind(this.name+":tokenizer:add",$.proxy(that.multiSelectChange,that));
        MF.vent.bind(this.name+":tokenizer:remove",$.proxy(that.multiSelectChange,that));
    },
    multiSelectChange: function(e){
        this.selectedViewmodel = this.item.getSelectedItems();
        this.validate();
    },
    getValue:function(){
        return this.item.getSelectedItems();
    },
    destroy:function(){
        MF.vent.unbind(this.name+":tokenizer:add");
        MF.vent.unbind(this.name+":tokenizer:remove");
        this.$container.off(this.$input.attr("id")+":tokenizer:blur");
        $("#"+this.$input.attr("name")+"_container *").unbind().remove();
        this._super("destroy",arguments);
    }
});

_.each(["Element", "Textbox", "Password","Select", "MultiSelect","PictureGallery","FileSubmission"], function(klass) {
    CC.Elements[klass].prototype._super = Backbone.View.prototype._super;
});