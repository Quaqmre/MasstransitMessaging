FROM rabbitmq:3.7-alpine

RUN rabbitmq-plugins enable --offline rabbitmq_management

# extract "rabbitmqadmin" from inside the "rabbitmq_management-X.Y.Z.ez" plugin zipfile
# see https://github.com/docker-library/rabbitmq/issues/207
RUN set -eux; \
    erl -noinput -eval ' \
    { ok, AdminBin } = zip:foldl(fun(FileInArchive, GetInfo, GetBin, Acc) -> \
    case Acc of \
    "" -> \
    case lists:suffix("/rabbitmqadmin", FileInArchive) of \
    true -> GetBin(); \
    false -> Acc \
    end; \
    _ -> Acc \
    end \
    end, "", init:get_plain_arguments()), \
    io:format("~s", [ AdminBin ]), \
    init:stop(). \
    ' -- /plugins/rabbitmq_management-*.ez > /usr/local/bin/rabbitmqadmin; \
    [ -s /usr/local/bin/rabbitmqadmin ]; \
    chmod +x /usr/local/bin/rabbitmqadmin; \
    apk add --no-cache python; \
    rabbitmqadmin --version

EXPOSE 15671 15672
EXPOSE 15672 15672
EXPOSE 5672 5672


