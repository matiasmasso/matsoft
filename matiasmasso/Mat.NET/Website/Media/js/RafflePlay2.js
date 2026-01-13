$(document).ready(function () {

	SISU_SignInOrSignUp($('.SISU'), '');
	LoadStoreLocator();

	$(document).on('SISU_Authenticated', function (e, argument) {
		$('#QuizRequest').before(spinner)
		var url = '/raffle/onAuthentication';
		var raffle = $('#raffleguid').val();
		var data = { raffle: raffle, email: argument };
		$.post(url, data)
			.done(function (result) {
				spinner.remove();
				if (result.success === 'true') {
					$('#token').val(result.token);
					//$('#FbVerificationRequest').show();
					$('#QuizRequest').show();
				} else {
					console.log(result.exception);
					$('.Exception span').html(result.exception);
				}
			})
			.fail(function (jqXHR, textStatus, errorThrown) {
				console.log("error " + textStatus);
				console.log("incoming Text " + jqXHR.responseText);
			});

	});

	$('.IsDuplicated a').click(function () {
		$('.IsDuplicated').hide();
		$('.SISU').empty();
		SISU_SignInOrSignUp($('.SISU'), $('#raffleguid').val());
	});

	/*
	$('#FbVerificationButton').click(function () {
		$('#QuizRequest').show();
	});
	*/

	$('#QuizRequest input[type="radio"]').click(function () {
		$('#DistributorSelection').show();
	});

	$('#ChangeDistributorRequest').click(function (event) {
		event.preventDefault();
		RequestToChangeDistributor();
	});

	$('#SubmitRequest input[type="button"]').click(function () {
		if ($('#termsAcceptance').is(":checked")) {
			$('#SubmitRequest input[type="button"]').attr("disabled", true);
			SubmitRaffle();
		}
		else
			alert('por favor acepte las condiciones');
	});

	$('#dropdownZona').change(function () {
		$('#dropdownLocation').show('fast');
		LoadDropdown($('#dropdownLocation'), SelectedZona().Locations, SelectedZona().DefaultLocation);
		$('#divDistributors').hide();
	});

	$('#dropdownLocation').change(function () {
		$('#dropdownDistributor').css("display", "block");
		LoadDistributors($('#divDistributors'), SelectedLocation());
		$('#divDistributors').show();
	});

	function LoadStoreLocator() {
		LoadDropdown($('#dropdownZona'), storelocator.DefaultCountry.Zonas, storelocator.DefaultZona);
	}


	function SelectedZona() {
		var guid = $('#dropdownZona option:selected').val();
		var retval = storelocator.DefaultCountry.Zonas.filter(function (item) { return item.Guid === guid; })[0];
		return retval;
	}

	function SelectedLocation() {
		var guid = $('#dropdownLocation option:selected').val();
		var retval = SelectedZona().Locations.filter(function (item) { return item.Guid === guid; })[0];
		return retval;
	}


	function LoadDropdown(dropdown, jsonObj, selectedObj = null) {
		dropdown.find("option:gt(0)").remove();
		//dropdown.empty();
		$.each(jsonObj, function (index, item) {
			var option = $('<option/>', {
				value: item.Guid,
				text: item.Nom,
			});
			if (selectedObj && item.Guid === selectedObj.Guid)
				option.selected = true
			dropdown.append(option);
		});
	}

	function LoadDistributors(div, selectedlocation) {
		div.empty();
		$.each(selectedlocation.Distributors, function (index, item) {
			LoadDistributor(div, item, selectedlocation)
		});

		if (selectedlocation.Distributors.length === 1) {
			var distributor = selectedlocation.Distributors[0];
			SelectDistributor(distributor, selectedlocation);
		}
	}

	function LoadDistributor(div, distributor, selectedlocation) {
		var divItem = $('<a href="#" class="Distributor" data-guid="' + distributor.Guid + '"/>')
		div.append(divItem);
		divItem.append(
			$('<div/>', {
				text: distributor.Nom,
				class: "Nom"
			}),
			$('<div/>', {
				text: distributor.Adr
			}),
			$('<div/>', {
				text: selectedlocation.Nom
			}),
			$('<div/>', {
				text: 'Tel.: ' + distributor.Tel
			}),
		);
	}

	$(document).on('click', '.Distributor', function (event) {
		event.preventDefault();
		var guid = $(this).data("guid");
		var selectedlocation = SelectedLocation();
		var distributor = selectedlocation.Distributors.filter(function (item) { return item.Guid === guid; })[0];
		SelectDistributor(distributor, selectedlocation);
	})

	function SelectDistributor(distributor, selectedlocation) {
		var div = $('#divDistributors');
		div.data('guid', distributor.Guid);
		div.empty();
		LoadDistributor(div, distributor, selectedlocation)
		$('#dropdownZona').hide('fast');
		$('#dropdownLocation').hide('fast');
		$('#ChangeDistributorRequest').show('fast');
		$('#SubmitRequest').show('fast');
	}



	function RequestToChangeDistributor() {
		$("#ChangeDistributorRequest").hide('fast');
		$('#divDistributors').hide();
		$('#SubmitRequest').hide();
		$('#dropdownZona option:first').attr("selected", true);
		$('#dropdownZona').show();
	}


	function SubmitRaffle() {
		$('#SubmitRequest input[type="button"]').attr("disabled", "disabled");
		$('#SubmitRequest input[type="button"]').after(spinner);
		var answer = $('#QuizRequest input[type="radio"]:checked').val();
		if (answer === null) answer = -1

		var url = '/raffle/Update';
		var data = {
			token: $('#token').val(),
			raffle: $('#raffleguid').val(),
			answer: $('#QuizRequest input[type="radio"]:checked').val(),
			distributor: $('#divDistributors').data('guid')
		};

		$('#Raffle_Steps').load(url, data, function () {
			spinner.remove();
		});
	}
});
