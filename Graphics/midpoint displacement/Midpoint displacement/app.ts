
class Main {
    element: HTMLElement;
    span: HTMLElement;
    timerToken: number;
    height = 400;
    width = 800;

    mountains: Mountain[];
    drawContext: Drawing;

    constructor(element: HTMLElement) {
        var canvas = document.createElement('canvas');
        canvas.id = "canvas";
        canvas.width = this.width;
        canvas.height =this. height;

        document.body.appendChild(canvas);

        var ctx = canvas.getContext('2d');
        this.drawContext = new Drawing(ctx);
        
        this.timerToken = setInterval(this.update.bind(this), 25);

        this.mountains = new Array<Mountain>();
    }

    start() {
        this.mountains.push(new Mountain(8, this.width, 100, this.height - 150, 1, 1.8, "rgb(130,150,130)", 3, 0));
        this.mountains.push(new Mountain(8, this.width, 75, this.height - 100, 2, 2.5, "rgb(80,100,80)", 2, 0));
        this.mountains.push(new Mountain(6, this.width, 50, this.height - 50, 2, 4, "rgb(40,50,40)", 1, 10));
    }

    draw() {
        var ctx = this.drawContext.Context;
        ctx.clearRect(0, 0, this.width, this.height);

        var black = "rgb(255,255,255)";
        var grd = ctx.createLinearGradient(this.width / 2, 0, this.width / 2, this.height/2);

        grd.addColorStop(0, "rgb(135,206,255)");
        grd.addColorStop(1, "white");

        ctx.fillStyle = grd;
        ctx.fillRect(0, 0, this.width, this.height);
        var stars: number = 50;

        this.mountains.forEach(mountain => mountain.draw(this.drawContext, this.drawContext.drawPath.bind(this.drawContext)));
    }

    update() {
        this.draw();
        this.mountains.forEach(mountain => mountain.update(this.timerToken));
        this.timerToken++;
    }

    stop() {
        clearTimeout(this.timerToken);
    }
}

class Drawing {
    Context: CanvasRenderingContext2D;

    constructor(context: CanvasRenderingContext2D) {
        this.Context = context;
    }

    drawPoint(size: number, point: Point, color: string) {
        if (this.Context == null || size == null || point == null) return;
        var ctx = this.Context;
        ctx.fillStyle = color;
        ctx.beginPath();
        ctx.arc(point.X, point.Y, size, 0, Math.PI * 2);
        ctx.fill();
    }

    drawLine(color, p1, p2) {
        var ctx = this.Context;
        ctx.beginPath();
        ctx.moveTo(p1.X, p1.Y);
        ctx.lineTo(p2.X, p2.Y);
        ctx.stroke();
    }

    drawPath(color, points, style) {
        var ctx = this.Context;
        ctx.fillStyle = style;
        ctx.beginPath();
        ctx.moveTo(points[0].X, points[0].Y);

        for (var t = 1; t < points.length; t++) {
            ctx.lineTo(points[t].X, points[t].Y);
        }
        // finish creating the rect so we can fill it
        ctx.lineTo(points[points.length - 1].X, this.Context.canvas.height);
        ctx.lineTo(points[0].X, this.Context.canvas.height);
        ctx.closePath();
        ctx.fill();
    }
}

class Mountain {
    Iterations: number;
    Width: number;
    Height: number;
    Y: number;
    Z: number;
    Roughness: number;
    Decay: number;
    Color: string;

    Points: Point[];
    NextFrame: Point[];
    Trees: Tree[];

    constructor(iterations: number, width: number, height: number, y: number, roughness: number, decay: number, color: string, z: number, treeNumber: number) {
        this.Iterations = iterations;
        this.Width = width;
        this.Height = height;
        this.Y = y;
        this.Roughness = roughness;
        this.Decay = decay;
        this.Color = color;

        this.Points = this.midpointDisplacement(0, this.Y - Math.random() * this.Height / 2);
        this.NextFrame = this.midpointDisplacement(this.Points[this.Points.length-1].X, this.Points[this.Points.length - 1].Y);
        this.Z = z;
        this.Trees = new Array<Tree>();
        for (var i = 0; i < treeNumber; i++) {
            var point = this.Points[Math.floor(Math.random() * this.Points.length)];
            this.Trees.push(new Tree(point, 40, this.Z));
        }
    }

    //  Create end points then iterate and add inbetween points
    midpointDisplacement(x, y) {
        var roughness = this.Roughness;
        var points = [];
        points[0] = new Point(x, y);

        points[1] = new Point(x + this.Width, y - Math.random() * this.Height / 2);
        var variance = 1;

        for (var i = 0; i < this.Iterations; i++) {
            var newPoints = [];
            //	go through each and add new point..
            for (var pointIndex = 0; pointIndex + 1 < points.length; pointIndex++) {
                var newPoint = Point.prototype.createMidpoint(points[pointIndex], points[pointIndex + 1], this.Height, roughness);
                newPoints.push(points[pointIndex]);
                newPoints.push(newPoint);
            }
            roughness /= this.Decay;
            newPoints.push(points[points.length - 1]);
            points = newPoints;
        }

        return points;
    }

    draw(drawing: Drawing, drawPath) {
        var ctx = drawing.Context;
        var bothFrames = [];
        this.Points.forEach(x => bothFrames.push(x));
        this.NextFrame.forEach(x => bothFrames.push(x));

        var highestPoint = bothFrames.reduce((prev, point) => {
            if (point.X > 0 && point.X < 800 && point.Y < prev) {
                return point.Y;
            }
            return prev;
        }, 1000);

        var lowestPoint = bothFrames.reduce((prev, point) => {
            if (point.X > 0 && point.X < 800 && point.Y > prev) {
                return point.Y;
            }
            return prev;
        }, 0);

        var midpoint = bothFrames[bothFrames.length / 2];
        var grd = ctx.createLinearGradient(midpoint.X, highestPoint, midpoint.X, 800);
        grd.addColorStop(0, this.Color);
        grd.addColorStop(1, "white");

        drawing.drawPath(this.Color, bothFrames, grd);

        this.Trees.forEach(t => {
            t.draw(drawing);
        });
    }

    update(timerToken: number) {
        if (timerToken % this.Z == 0) {
            var lastPoint = this.Points[this.Points.length - 1];
            if (lastPoint.X <= 0) {
                //  Swap the buffers and regenerate 'nextFrame'
                this.Points = this.NextFrame;
                lastPoint = this.Points[this.Points.length - 1];
                this.NextFrame = this.midpointDisplacement(lastPoint.X, lastPoint.Y);
            } else {
                this.Points.forEach(function (p) {
                    p.X--
                });
                this.Trees.forEach(t => t.Position.X--);
                this.NextFrame.forEach(p => p.X--);
            }
        }
    }
}

class Tree {
    Position: Point;
    Height: number;
    Z: number;

    constructor(position: Point, height: number, z: number) {
        this.Position = position.clone().add(0, Math.random()*50);
        this.Height = height;
        this.Z = z;
    }

    draw(ctx: Drawing) {
        ctx.drawLine("rgb(0,0,0)", this.Position, this.Position.add(0, -this.Height));
    }
}

class Point {
    X: number;
    Y: number;

    constructor(x: number, y: number) {
        this.X = x;
        this.Y = y;
    }

    createMidpoint(p1: Point, p2: Point, height: number, roughness: number) {
        var midPointX = (p1.X + p2.X) / 2;
        //  Average of points plus a number that should go between  -height -> +height
        var midPointY = (p1.Y + p2.Y) / 2 + ( 2 * Math.random() * height - height) * roughness;
        
        if (midPointY < 0) midPointY = 0;

        return new Point(midPointX, midPointY);
    }

    addPoint(p: Point) {
        var p = new Point(this.X + p.X, this.Y + p.Y);
        return p;
    }

    add(x: number, y: number) {
        var p = new Point(this.X + x, this.Y + y);
        return p;
    }

    clone() {
        return new Point(this.X, this.Y);
    }
}

window.onload = () => {
    var el = document.getElementById('content');
    var greeter = new Main(el);
    greeter.start();
};