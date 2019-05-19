using System;

namespace Deribit.S4KTNET.Core.SessionManagement
{
    // https://docs.deribit.com/v2/#private-enable_cancel_on_disconnect

    public class EnableCancelOnDisconnectResponse : ResponseBase
    {
        public bool result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<EnableCancelOnDisconnectResponseDto, EnableCancelOnDisconnectResponse>()
                    .ForMember(x => x.result, o => o.MapFrom(x => x.result == "ok"));

                this.CreateMap<string, EnableCancelOnDisconnectResponse>()
                    .ConvertUsing((s, d, r) =>
                    {
                        return r.Mapper.Map<EnableCancelOnDisconnectResponse>(r.Mapper.Map<EnableCancelOnDisconnectResponseDto>(s));
                    });
            }
        }

    }

    public class EnableCancelOnDisconnectResponseDto
    {
        public string result { get; set; }

        internal class Profile : AutoMapper.Profile
        {
            public Profile()
            {
                this.CreateMap<string, EnableCancelOnDisconnectResponseDto>()
                    .ConvertUsing(s => new EnableCancelOnDisconnectResponseDto()
                    {
                        result = s,
                    });
            }
        }
    }
}