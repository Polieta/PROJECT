package request

type UpdateTagsRequest struct {
	Id   int    `validate:"requires"`
	Name string `validate:"required,max=200,min=1" json:"name"`
}
