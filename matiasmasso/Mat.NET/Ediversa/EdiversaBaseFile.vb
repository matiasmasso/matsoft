Public Class EdiversaBaseFile

    Public Property interlocutors As List(Of Interlocutor)

    Public Sub New()
        _interlocutors = New List(Of Interlocutor)
    End Sub

    Public Function AddInterlocutor(oCod As Interlocutor.cods) As Interlocutor
        Dim retval As New Interlocutor(oCod)
        _interlocutors.Add(retval)
        Return retval
    End Function

    Shared Function segments(src As String) As List(Of Segment)
        Return src.Split(vbCrLf).Select(Function(x) Segment.factory(x)).ToList
    End Function


    Public Class Segment
        Property values As List(Of String)

        Shared Function factory(src As String) As Segment
            Dim retval As New Segment
            retval.values = src.Split("|").ToList
            Return retval
        End Function

        Public Function tag() As String
            Dim retval As String = ""
            If _values.Count > 0 Then
                retval = _values.First.ToUpper
            End If
            Return retval
        End Function

        Public Function stringValue(fieldIdx As Integer) As String
            Dim retval As String = ""
            If _values.Count > fieldIdx Then
                retval = _values(fieldIdx)
            End If
            Return retval
        End Function

        Public Function decimalValue(fieldIdx As Integer) As Decimal
            Dim retval As Decimal
            If _values.Count > fieldIdx Then
                Dim culture As IFormatProvider = Globalization.CultureInfo.InvariantCulture
                retval = Decimal.TryParse(_values(fieldIdx), Globalization.NumberStyles.Float, culture, retval)
            End If
            Return retval
        End Function

        Public Function integerValue(fieldIdx As Integer) As Integer
            Dim retval As Integer
            If _values.Count > fieldIdx Then
                Integer.TryParse(_values(fieldIdx), retval)
            End If
            Return retval
        End Function

        Public Function eanValue(fieldIdx As Integer, exs As List(Of Exception)) As Ean
            Dim retval As New Ean
            If _values.Count > fieldIdx Then
                retval = Ean.factory(_values(fieldIdx), exs)
            End If
            Return retval
        End Function

        Public Function dateValue(fieldIdx As Integer, exs As List(Of Exception)) As Date
            Dim retval As Date
            If _values.Count > fieldIdx Then
                Dim src = stringValue(fieldIdx)
                Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
                Try
                    Select Case src.Length
                        Case 0
                        Case 6
                            retval = Date.ParseExact(src, "yyMMdd", provider)
                        Case 8
                            retval = Date.ParseExact(src, "yyyyMMdd", provider)
                        Case 12
                            retval = Date.ParseExact(src, "yyyyMMddHHmm", provider)
                        Case Else
                            exs.Add(New Exception(String.Format("No es pot convertir {0} en una data", src)))
                    End Select

                Catch ex As Exception
                    exs.Add(ex)
                End Try

                Return retval
            End If
            Return retval
        End Function
    End Class

    Public Class Ean
        Property value As String

        Shared Function factory(src As String, exs As List(Of Exception)) As Ean
            Dim retval As New Ean
            retval.value = src
            Return retval
        End Function
    End Class

    Public Class Interlocutor
        Property cod As cods
        Property ean As Ean
        Property nom As String
        Property refMercantil As String
        Property domicilio As String
        Property poblacion As String
        Property zip As String
        Property nif As String
        Property pais As String
        Property codProveedor As String 'assignat per el comprador
        Property codCliente As String 'assignat per el proveidor
        Property seccion As String
        Property aprobacion As String

        Public Enum cods
            proveedor
            cliente
            emisor
            receptor
            emisorMsg
            receptorMsg
            receptorMercancia
            receptorFinalMercancia
            emisorPago
            receptorPago
            recogidaMercancia
            forwarder
        End Enum

        Public Sub New(oCod As cods)
            MyBase.New
            _cod = oCod
        End Sub
    End Class
End Class
