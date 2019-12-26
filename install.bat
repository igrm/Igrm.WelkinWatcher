kubectl create secret docker-registry regcred --docker-server=de.icr.io/igrm/welkinwatcher --docker-username=iamapikey --docker-password=<tbd> --docker-email=<tbd>
kubectl apply -f RabbitMQ.deployment.yml
kubectl apply -f RabbitMQ.service.yml
kubectl apply -f BackgroundWorker.deployment.yml
kubectl apply -f SignalR.deployment.yml
kubectl apply -f SignalR.service.yml
kubectl apply -f InfoBoard.deployment.yml
kubectl apply -f InfoBoard.service.yml