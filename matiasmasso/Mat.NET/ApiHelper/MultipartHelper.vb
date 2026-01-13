Imports System.Net.Http

Public Class MultipartHelper

    Property Items As List(Of Item)

    Public Sub New()
        MyBase.New
        _Items = New List(Of Item)
    End Sub


    Public Sub AddStringContent(key As String, value As String)
        Dim item As New Item
        With item
            .Cod = Item.Cods.StringContent
            .key = key
            .stringContent = value
        End With
        _Items.Add(item)
    End Sub

    Public Sub AddFileContent(key As String, oByteArray As Byte())
        Dim item As New Item
        With item
            .Cod = Item.Cods.FileContent
            .key = key
            .filename = key
            .FileContent = oByteArray
        End With
        _Items.Add(item)
    End Sub

    Public Function FormContent() As MultipartFormDataContent
        Dim retval As New MultipartFormDataContent()
        For Each item In Items
            Select Case item.Cod
                Case Item.Cods.StringContent
                    If item.stringContent > "" Then
                        retval.Add(New Net.Http.StringContent(item.stringContent), item.key)
                    End If
                Case Item.Cods.FileContent
                    If item.FileContent IsNot Nothing Then
                        retval.Add(New Net.Http.StreamContent(New IO.MemoryStream(item.FileContent)), item.key, item.filename)
                    End If
            End Select
        Next
        Return retval
    End Function

    Public Class Item
        Property Cod As Cods
        Property FileContent As Byte()
        Property key As String
        Property filename As String
        Property stringContent As String

        Public Enum Cods
            StringContent
            FileContent
        End Enum

    End Class
End Class
