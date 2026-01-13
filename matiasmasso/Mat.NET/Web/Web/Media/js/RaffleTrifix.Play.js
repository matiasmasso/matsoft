$(document).ready(function () {

	//$.getScript("/Media/js/SignInOrSignUp.js")
		//.done(function (script, textStatus) {
			//console.log("done callback");
		    SISU_SignInOrSignUp($('.SISU'), '');



		    $(document).on('SISU_Authenticated', function (e, argument) {
		        $.getJSON(
					'/Sorteos/onAuthentication',
					{ raffle: $('#raffleguid').val(), email: argument },
					function (result) {
						if (result.isDuplicated == 'true') {
							$('.IsDuplicated span').html(result.fch);
							$('.IsDuplicated').show();
						} else {
							$('#token').val(result.token);
							$('#FbVerificationRequest').show();
						}
					}
				)

			});

			$('.IsDuplicated a').click(function () {
				$('.IsDuplicated').hide();
				$('.SISU').empty();
				SISU_SignInOrSignUp($('.SISU'), $('#raffleguid').val());
			});

			$('#FbVerificationButton').click(function () {
			    $('#QuizRequest').show();
			    $('#SubmitRequest').show();
			});


			$(document).on('click','#SubmitRequest input[type="button"]',function () {
				if ($('#termsAcceptance').is(":checked"))
					SubmitRaffle();
				else
					alert('por favor acepte las condiciones')
			});


			function SubmitRaffle() {
				$('#SubmitRequest input[type="button"]').attr("disabled", "disabled");
				$("div.loading").show();
				var answer = $('#QuizRequest input[type="radio"]:checked').val();
				if (answer == null) answer = -1

				$.getJSON(
					'/Sorteo-Trifix/Update',
					{
						token: $('#token').val(),
						Raffle: $('#raffleguid').val(),
						code: $('#QuizRequest input[type="text"]').val()
					},
					function (result) {
						$("div.loading").hide();
						if (result.success == 1) {
						    $("#Raffle_Steps").hide();
						    $("#Raffle_Thanks").show();

						    $.getJSON(
								'/Sorteos/MailConfirmation',
								{
								    raffleparticipant: result.participant
								}
							);

						}
						else if (result.success == 3) {
						    alert('Error: el código introducido es incorrecto');
						    $('#SubmitRequest input[type="button"]').removeAttr("disabled");
						}
						else {
							alert('Error: Lamentamos informarle de que no ha sido posible registrar su participación por un error técnico. Por favor comuníquelo en el stand con el número de error SYSERR_061 y disculpe las molestias');
						}
					}
				)
			}

		//})
		//.fail(function (jqxhr, settings, exception) {
		//	console.log("fail callback");
		//});
})

