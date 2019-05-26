﻿using System;
using System.Threading;
using Deribit.S4KTNET.Core.Mapping;
using Deribit.S4KTNET.Core.MarketData;
using Deribit.S4KTNET.Core.Trading;
using FluentValidation;

namespace Deribit.S4KTNET.Core.SubscriptionManagement
{
    // https://docs.deribit.com/v2/#user-orders-kind-currency-interval

    public class UserOrdersNotification : SubscriptionNotification<Order[]>
    {
        internal class Profile : AutoMapper.Profile
        {
            private int sequencenumber = 1 << 10;

            public Profile()
            {
                this.CreateMap<UserOrdersNotificationDto, UserOrdersNotification>()
                    .ForMember(d => d.channelprefix, o => o.MapFrom(s => DeribitChannelPrefix.userorders))
                    .ForMember(d => d.sequencenumber, o => o.MapFrom(s => Interlocked.Increment(ref sequencenumber)))
                    .IncludeBase(typeof(SubscriptionNotificationDto<OrderDto[]>), typeof(SubscriptionNotification<Order[]>));
            }
        }

        internal class Validator : FluentValidation.AbstractValidator<UserOrdersNotification>
        {
            public Validator()
            {
                this.RuleFor(x => x.channelprefix).Equal(DeribitChannelPrefix.userorders);
                this.RuleForEach(x => x.data).SetValidator(new Order.Validator());
            }
        }
    }

    internal class UserOrdersNotificationDto : SubscriptionNotificationDto<OrderDto[]>
    {
        
    }
}
