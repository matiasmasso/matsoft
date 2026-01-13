Public Class DTOEdiversaInterlocutor

    Property Ean As String
    Property Nom As String
    Property Address As String
    Property Location As String
    Property Zip As String


    Public Sub New(segment As String)
        MyBase.New
        Dim fields As List(Of String) = segment.Split("|").ToList
        If fields.Count > 1 Then
            _Ean = fields(1)
            If fields.Count > 2 Then
                _Nom = fields(2)
                If fields.Count > 3 Then
                    _Address = fields(3)
                    If fields.Count > 4 Then
                        _Location = fields(4)
                        If fields.Count > 5 Then
                            _Zip = fields(5)
                        End If
                    End If
                End If
            End If
        End If
    End Sub


End Class
