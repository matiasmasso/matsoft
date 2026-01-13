

Public Class SpriteHelper

    Public Const MAXCOLUMNS As Integer = 10


    Shared Function Factory(url As String, itemWidth As Integer, itemHeight As Integer) As Sprite
        Dim retval As New Sprite
        With retval
            .url = url
            .itemWidth = itemWidth
            .itemHeight = itemHeight
            .items = New List(Of Sprite.Item)
        End With
        Return retval
    End Function

    Public Class Sprite
        Property url As String
        Property itemWidth As Integer
        Property itemHeight As Integer
        Property items As List(Of Item)

        Private _col As Integer
        Private _row As Integer


        Public Function addItem(caption As String, url As String) As Item
            Dim retval As New Item
            With retval
                .idx = _items.Count
                .width = _itemWidth
                .height = _itemHeight
                .caption = caption
                .url = url
                .col = _col
                .row = Math.Truncate(.idx / MAXCOLUMNS)
            End With
            _items.Add(retval)
            _col = If(_col + 1 = MAXCOLUMNS, 0, _col + 1)
            Return retval
        End Function


        Public Class Item
            Property idx As Integer
            Property col As Integer
            Property row As Integer
            Property width As Integer
            Property height As Integer
            Property caption As String
            Property url As String

            Public Function offsetX() As Integer
                Return -_col * _width
            End Function

            Public Function offsetY() As Integer
                Return -_row * _height
            End Function
        End Class
    End Class
End Class