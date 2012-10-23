var dataTypes = '';
var dataTypesMap = {};
var locations = '';
var locationsMap = {};

function showAddForm(event){
    //show add new form
      $('#addArea').val(event.key);
      $('#addDate').val(new Date());
      $('#addDose').val('');
      $('#addFraction').val('');
      $('#addForm').lightbox_me({centered: true});
}

function highlight_locations(id){   
    var options = {
            mapKey: "data-key",
            fillOpacity: 0,
            render_select: {
                fillColor: 'E4EB28',
                fillOpacity: 0.2,
                stroke: true
            },
            render_highlight: {
                strokeWidth: 2,
                fillColor: 'ff0000',
                fillOpacity: 0.3,
                stroke: true
            },
            onClick: function(event){                               
                    var entries = locationsMap[event.key];
                    $("#details").empty();       
                    if(entries){
                        $.each(entries, function(index, field){                     
                            $("#details").append("Date: " + new Date(parseInt(field.TreatmentDate.slice(6, -2))) + "<br/>");
                            $.each(field.AffectedRegions, function(index, region){
                                $("#details").append("Type: " + region.type + " "); 
                                $("#details").append("Location: " + region.Location + " ");
                                $("#details").append("Dose: " + region.dose + "Gy<br/>");
                                $("#details").append("Fractions: " + field.Fractions + "<br/>");
                            });
                        });
                    
                        $("#details").append('<a class="close" href="#">Close</a>');
                        $('#details').append('<a id="add" href="#">Enter new</a>');
                        $('#add').click(function(){
                            showAddForm(event);
                        });
                        $("#details").lightbox_me({centered: true});
                    }else{
                        showAddForm(event);
                    }
                    event.stopPropagation(); //halt deselect
            }          
        };
    $(id).mapster(options);
    $(id).mapster('set', true, locations);
}



var onSuccess = function(data){    
    //console.log(data);
    dataTypes = '';
    dataTypesMap = {};
    locations = '';
    locationsMap = {};
    $("#patients").empty();
    $("#patients").append("<hr/>");
    if(data.MRN == null){
        $("#patients").append("No matching patient found.");
        return;
    }
    $("#patients").append(data.Firstname + " " + data.Lastname + "<br/>");
    
    $.each(data.Fields, function(index, field){
        
        $("#patients").append("Date: " + new Date(parseInt(field.TreatmentDate.slice(6, -2))) + "<br/>");
        $.each(field.AffectedRegions, function(index, region){
            $("#patients").append("Type: " + region.type + " ");
            dataTypes += region.type + ',';         
            dataTypesMap[region.type] = (dataTypesMap[region.type] || 0);
            dataTypesMap[region.type] += 1;
            $("#patients").append("Location: " + region.Location + " ");
            locations += region.Location +',';
            locationsMap[region.Location] = (locationsMap[region.Location] || []);
            locationsMap[region.Location].push(field);
            $("#patients").append("Dose: " + region.dose + "Gy ");
            $("#patients").append("Fractions: " + field.Fractions + "<br/>");
        });
    });
    
    $("#skeletonImageMap").show();
    var options = {
            mapKey: "data-key",
            fillOpacity: 0,
            render_select: {
                fillColor: 'ff0000',
                fillOpacity: 0.2,
                stroke: true
            },
            render_highlight: {
                strokeWidth: 2,
                fillColor: '0000ff',
                fillOpacity: 0.3,
                stroke: true
            },
            onClick: function(event){               
                if(event.key == "spine"){
                    $('#spine').lightbox_me();
                    highlight_locations('#spine_image');
                }else if (event.key == "ribs"){                 
                    $('#ribs').lightbox_me();
                    highlight_locations('#ribs_image');
                }else if (event.key == "shoulder"){
                    $('#shoulder').lightbox_me();
                }else if (event.key =="pelvis"){
                    $('#pelvis').lightbox_me();
                } else {
                    alert(event.key);
                }
                event.stopPropagation();
            }          
        };
    $("#skeleton").mapster(options);
    //$("#skeleton").mapster('set', true, dataTypes);
    $.each(dataTypesMap, function(key, index){
        var selectedAreaOptions = {staticState: true, 
                render_select:{fillOpacity: 0.3 * dataTypesMap[key]}};
        var selector = 'area[data-key="'+key+'"]';
        $(selector).mapster('set', true, selectedAreaOptions); //fixme: this does not select the area
    });
};

var onError = function(response, textstatus, errorthrown){
    //alert('error');
    //console.log(response);
    //console.log(textstatus);
    //console.log(errorthrown);
    $("#error").append(error.statusText);
};

function patientSearch(){
    var mrn = $("#mrn").val();
    var url = "patients";
    //var url = "http://winhacker:100/BoneMets/GetPatient";
    var type = "json";
    var data = { mrn: mrn };
    
    $.ajax({
          type: 'POST',
          url: url,
          data: data,
          success: onSuccess,
          error: onError,
          dataType: type
        });
}
