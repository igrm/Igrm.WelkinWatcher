kubectl apply secret docker-registry regcred --docker-server=de.icr.io/igrm/welkinwatcher --docker-username=<your-name> --docker-password=<your-pword> --docker-email=<your-email>
kubectl apply -f RabbitMQ.deployment.yml
kubectl apply -f RabbitMQ.service.yml
kubectl apply -f BackgroundWorker.deployment.yml
kubectl apply -f SignalR.deployment.yml
kubectl apply -f SignalR.service.yml
kubectl apply -f InfoBoard.deployment.yml
kubectl apply -f InfoBoard.service.yml