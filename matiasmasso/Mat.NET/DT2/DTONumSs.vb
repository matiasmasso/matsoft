Public Class DTONumSs
    Private _cleanValue As String

    ReadOnly Property cleanValue As String
        Get
            Return _cleanValue
        End Get
    End Property

    Public Sub New(rawValue As String)
        _cleanValue = Clean(rawValue)
    End Sub

    Public Function IsValid(exs As List(Of Exception)) As Boolean
        Return Validate(_cleanValue, exs)
    End Function

    Public Function Formatted() As String
        Dim retval As String = String.Format("{0}/{1}/{2}", Provincia(_cleanValue), Core(_cleanValue), ControlDigits(_cleanValue))
        Return retval
    End Function

    Public Shadows Function Equals(candidate As String) As Boolean
        Dim sCleanCandidate = Clean(candidate)
        Dim retval = sCleanCandidate = _cleanValue
        Return retval
    End Function

#Region "Utilities"


    Private Shared Function Clean(sRawNumSS As String) As String
        Dim retval As String = TextHelper.LeaveJustNumbericDigits(sRawNumSS)
        Return retval
    End Function

    Private Shared Function Provincia(sCleanNumSS As String) As String
        Dim retval As String = ""
        If sCleanNumSS.Length > 2 Then
            retval = sCleanNumSS.Substring(0, 2)
        End If
        Return retval
    End Function

    Private Shared Function Core(sCleanNumSS As String) As String
        Dim retval As String = ""
        If sCleanNumSS.Length > 4 Then
            retval = sCleanNumSS.Substring(2, sCleanNumSS.Length - 4)
        End If
        Return retval
    End Function

    Private Shared Function ControlDigits(sCleanNumSS As String) As String
        Dim retval As String = ""
        If sCleanNumSS.Length > 4 Then
            retval = sCleanNumSS.Substring(sCleanNumSS.Length - 2)
        End If
        Return retval
    End Function



    Private Shared Function Validate(src As String, exs As List(Of Exception)) As Boolean
        'El número de afiliación a la Seguridad Social tiene el siguiente formato: 
        'Código de la provincia donde se asigna el número de la Seguridad Social al trabajador o empresa (2 dígitos)
        'Número secuencial asignado (7 u 8 dígitos según sea una empresa o un trabajador)
        'Dígitos de control (2 dígitos)
        Dim retval As Boolean
        Dim cleanResult As String = Clean(src)
        If cleanResult.Length = 10 Or cleanResult.Length = 11 Or cleanResult.Length = 12 Then
            Dim sProvincia As String = Provincia(cleanResult)
            Dim sCore As String = Core(cleanResult)
            Dim sControlDigits As String = ControlDigits(cleanResult)
            Dim sCheckedControlDigits As String = CalcControlDigits(sProvincia & sCore)
            If sCheckedControlDigits = sControlDigits Then
                retval = True
            Else
                exs.Add(New Exception("El número Seg.Social '" & src & "' te els digits de control erronis"))
            End If
        Else
            exs.Add(New Exception("El número Seg.Social '" & src & "' no te la longitut correcte"))
        End If
        Return retval
    End Function



    Private Shared Function CalcControlDigits(ByVal numSegSocial As String,
                                  Optional ByVal esNumEmpresa As Boolean = False) As String

        ' Si hay más de 10 dígitos en el número se devolverá una excepción de
        ' argumentos no permitidos.
        '
        If (numSegSocial.Length > 10) OrElse (numSegSocial.Length = 0) Then _
            Throw New System.ArgumentException()

        ' Si algún carácter no es un número, abandono la función.
        '
        Dim regex As New System.Text.RegularExpressions.Regex("[^0-9]")
        If (regex.IsMatch(numSegSocial)) Then _
            Throw New System.ArgumentException()

        Try
            ' Obtengo el número correspondiente a la Provincia
            '
            Dim dcProv As String = numSegSocial.Substring(0, 2)

            ' Obtengo el resto del número
            '
            Dim numero As String = numSegSocial.Substring(2, numSegSocial.Length - 2)

            Select Case numero.Length
                Case 8
                    If (esNumEmpresa) Then
                        ' Si el número es de una empresa, no puede tener 8 dígitos.
                        Return String.Empty

                    Else
                        ' Compruebo si es un NAF nuevo o antiguo.
                        If (numero.Chars(0) = "0"c) Then
                            ' Es un número de afiliación antiguo. Lo formateo
                            ' a siete dígitos, eliminando el primer cero.
                            numero = numero.Remove(0, 1)

                        End If

                    End If

                Case 7
                    ' Puede ser un NAF antiguo o un CCC nuevo o viejo.
                    If (esNumEmpresa) Then
                        ' Si el primer dígito es un cero, es un CCC antiguo,
                        ' por lo que lo formateo a seis dígitos, eliminando
                        ' el primer cero.
                        If (numero.Chars(0) = "0"c) Then
                            numero = numero.Remove(0, 1)
                        End If
                    End If

                Case 6
                    ' Si se trata del número de una empresa,
                    ' es un CCC antiguo.
                    If (Not (esNumEmpresa)) Then
                        ' Es un NAF antiguo, por lo que lo debo
                        ' de formatear a 7 dígitos.
                        numero = numero.PadLeft(7, "0"c)
                    End If

                Case Else
                    ' Todos los demás casos, serán números antiguos
                    If (esNumEmpresa) Then
                        ' Lo formateo a seis dígitos.
                        numero = numero.PadLeft(6, "0"c)

                    Else
                        ' Lo formateo a siete dígitos.
                        numero = numero.PadLeft(7, "0"c)

                    End If

            End Select

            ' Completo el número de Seguridad Social
            '
            Dim naf As Int64 = Convert.ToInt64(dcProv & numero)

            ' Calculo el Dígito de Control. Tengo que operar con números
            ' Long, para evitar el error de desbordamiento que se puede
            ' producir con los nuevos números de Seguridad Social
            '
            naf = naf - (naf \ 97) * 97

            ' Devuelvo el Dígito de Control formateado
            '
            Return String.Format("{0:00}", naf)

        Catch
            Return String.Empty

        End Try

    End Function
#End Region

End Class
