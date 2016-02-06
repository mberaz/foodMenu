var webApi = function () {
    var self = this;

    self.ajax=function(url,options)
    {
        var api = self.apiConnectionData;//base api
        var headers = {
            'client_id': api.userId,
            'access_token': api.accessToken(),
            'content-type': 'application/json'
        };

        var endOptions = $.extend({}, options, {
            url: api.URL + actionUrl,
            headers: headers

        });

        return  $.ajax(endOptions) ;

    }

   // ajax().done(function (privData) { })
    //type: "POST",
    //data: ko.toJSON(quotamodel),
    //cache: false,
    //beforeSend: clickd.showLoader


    return self;
};