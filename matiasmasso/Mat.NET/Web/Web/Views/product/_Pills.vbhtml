@Code
    
End Code

<nav>
    <ul>
        @For Each item As DTONavbarItem In FEB2.Product.Pills(ViewBag.Product, Mvc.ContextHelper.lang(), ViewBag.Tab).Items
            @<li 
                  @If item.Selected Then
                    @:class="selected"
                  End If
            >

            <a                   
                 @If item.Id > "" Then
                    @: id="@item.Id"
                 End If   
                href='@item.NavigateTo'>@item.Title</a>



            @If item.Items.Count > 0 Then
                @<ul>
                    @For Each subitem As DTONavbarItem In item.Items
                        @<li 
                              @If subitem.Selected Then
                                @:class="selected"
                              End If
                        >

                             <a 
                                @If subitem.Id > "" Then
                                    @:id="@subitem.Id"
                                End If
                                href='@subitem.NavigateTo'>@subitem.Title</a>
                        </li>
                    Next
                </ul>               
            End If
            </li>
        Next

        </ul>
    </nav>

