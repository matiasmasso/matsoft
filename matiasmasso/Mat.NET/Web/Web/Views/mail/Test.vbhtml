@ModelType System.Web.Mvc.JsonResult
@Code
    Layout = "~/Views/Mail/_Layout3.vbhtml"
End Code


<div class="CustomerSelection">
    <div class="Country">
        <select></select>
    </div>
    <div class="Zona">
        <select></select>
    </div>
    <div class="Location">
        <select></select>
    </div>
    <div class="Customer">
        <select></select>
    </div>
</div>

<script>
    var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model));

    var $select=$('.CustomerSelection .Country select');
    loadDropdown($select,countries());


    $(document).on('change','.CustomerSelection .Country select',function(e){
        var $select=$('.CustomerSelection .Zona select');
        loadDropdown($select,zonas());
    });

    $(document).on('change','.CustomerSelection .Zona select',function(e){
        var $select=$('.CustomerSelection .Location select');
        loadDropdown($select,locations());
    });

    $(document).on('change','.CustomerSelection .Location select',function(e){
        var $select=$('.CustomerSelection .Customer select');
        loadDropdown($select,customers());
    });

    function loadDropdown(select,items) {
        select.find('option').remove();
        $.each(items, function(key, value) {
            $('<option>').val(value.Guid).text(value.Nom).appendTo(select);
        });
    }

    function countries() {
        return model.Data;
    }

    function zonas() {
        var country=$('.CustomerSelection .Country select').val();
        var retval;
        $.each(model.Data, function(key, value) {
            if(value.Guid == country) {
                retval=value.Items;
                return false;
            }
        });
        return retval;
    }
    function locations() {
        var zona=$('.CustomerSelection .Zona select').val();
        var retval;
        $.each(zonas(), function(key, value) {
            if(value.Guid == zona) {
                retval=value.Items;
                return false;
            }
        });
        return retval;
    }
    function customers() {
        var location=$('.CustomerSelection .Location select').val();
        var retval;
        $.each(locations(), function(key, value) {
            if(value.Guid == location) {
                retval=value.Items;
                return false;
            }
        });
        return retval;
    }
</script>

